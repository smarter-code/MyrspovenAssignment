using AutoMapper;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MyrspovenAssignment.Config;
using MyrspovenAssignment.Infrastructure;
using MyrspovenAssignment.Infrastructure.Repositories;
using MyrspovenAssignment.ViewModels;
namespace MyrspovenAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRepository<Building> buildingRepository;
        private readonly IRepository<SignalData> signalDataRepository;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;
        private readonly IConfiguration configuration;
        private readonly ApiSettings apiSettings;

        public HomeController(ILogger<HomeController> logger,
            IRepository<Building> buildingRepository,
            IRepository<SignalData> signalDataRepository, IMapper mapper, IMemoryCache cache, IConfiguration configuration)
        {
            this.logger = logger;
            this.buildingRepository = buildingRepository;
            this.signalDataRepository = signalDataRepository;
            this.mapper = mapper;
            this.cache = cache;
            this.configuration = configuration;
            apiSettings = new ApiSettings
            {
                BaseUrl = configuration["ApiSettings:BaseUrl"],
                ClientId = configuration["ApiSettings:ClientId"],
                ClientSecret = configuration["ApiSettings:ClientSecret"],
                GrantType = configuration["ApiSettings:GrantType"],
                TokenUrl = configuration["ApiSettings:TokenUrl"]
            };
        }

        public async Task<IActionResult> Index()
        {
            // Access Token
            Token token = await GetAccessToken();


            //Buildings
            var buildingsViewModels = await GetBuildings(token);
            var buildings = mapper.Map<List<Building>>(buildingsViewModels);
            SaveBuildings(buildings);

            //Signals
            await PopulateSignalsPerBuilding(buildingsViewModels, token);


            //Signals Data
            var singalsDataPerBuildingViewModels = await GetAndPopulateSignalDataPerBuilding(buildingsViewModels, token);
            var singalsDataPerBuilding = mapper.Map<List<SignalData>>(singalsDataPerBuildingViewModels);
            SaveSingalsDataPerBuilding(singalsDataPerBuilding);


            return Ok(buildingsViewModels);
        }

        private async Task SaveSingalsDataPerBuilding(List<SignalData> singalsDataPerBuilding)
        {
            foreach (var signalData in singalsDataPerBuilding)
            {
                // Not super sure, but is the only way to check if I had the same signal before
                // This is based on the assumption that ReadUtc will never be null
                signalDataRepository.AddIfNotExists(signalData,
                    s => s.Value == signalData.Value &&
                    DateTime.Equals(s.ReadUtc, signalData.ReadUtc)
                     && s.BuildingId == signalData.BuildingId);
            }
            signalDataRepository.SaveChanges();

        }

        private void SaveBuildings(List<Building> buildings)
        {
            foreach (var building in buildings)
            {
                buildingRepository.AddIfNotExists(building, b => b.Id == building.Id);
            }
            buildingRepository.SaveChanges();
        }

        private async Task<List<SignalDataViewModel>> GetAndPopulateSignalDataPerBuilding(List<BuildingViewModel> buildings, Token token)
        {
            var allSignalsData = new List<SignalDataViewModel>();
            foreach (var building in buildings)
            {
                var signalsResponse = await apiSettings.BaseUrl
                     .AppendPathSegment("Signal/GetSignalValues")
                    .WithOAuthBearerToken(token.access_token)
                   .PostJsonAsync(new { buildingId = building.Id });
                var signalsData = await signalsResponse.GetJsonAsync<List<SignalDataViewModel>>().ConfigureAwait(true);
                signalsData.ToList().ForEach(signal => signal.BuildingId = building.Id);
                var signalsDataWithBuildingIdDuringLastHour = signalsData.Where(signalData => (DateTime.UtcNow - DateTime.Parse(signalData.ReadUtc)).TotalMinutes <= 60);
                building.SignalsData = signalsDataWithBuildingIdDuringLastHour.ToList();
                allSignalsData.AddRange(signalsData);

            }
            return allSignalsData;
        }

        private async Task PopulateSignalsPerBuilding(List<BuildingViewModel> buildings, Token token)
        {
            var allSingals = new List<SignalViewModel>();
            foreach (var building in buildings)
            {
                var signalsResponse = await apiSettings.BaseUrl
                     .AppendPathSegment("Signal/GetSignals")
                    .WithOAuthBearerToken(token.access_token)
                   .PostJsonAsync(new { buildingId = building.Id });
                var singals = await signalsResponse.GetJsonAsync<List<SignalViewModel>>().ConfigureAwait(true);
                building.Signals = singals;
            }
        }

        private async Task<List<BuildingViewModel>> GetBuildings(Token token)
        {
            var result = await apiSettings.BaseUrl
                  .AppendPathSegment("Building/GetBuildings")
                  .WithOAuthBearerToken(token.access_token)
                  .PostAsync();

            return await result.GetJsonAsync<List<BuildingViewModel>>().ConfigureAwait(true);
        }

        private async Task<Token> GetAccessToken()
        {
            var token = new Token();
            if (!cache.TryGetValue($"{apiSettings.ClientId}-token", out token))
            {
                token = await apiSettings.TokenUrl
              .PostUrlEncodedAsync(
               new
               {
                   client_id = apiSettings.ClientId,
                   client_secret = apiSettings.ClientSecret,
                   grant_type = apiSettings.GrantType
               }).ReceiveJson<Token>();

                var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(
                              TimeSpan.FromSeconds(token.expires_in));
                cache.Set($"{apiSettings.ClientId}-token", token, cacheOptions);
            }
            return token;

        }

    }
}