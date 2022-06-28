﻿using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using MyrspovenAssignment.Models;
using System.Diagnostics;
namespace MyrspovenAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await "https://i4test.azurewebsites.net/connect/token"
               .PostUrlEncodedAsync(
                new
                {
                    client_id = "recruit",
                    client_secret = "ey9nf7E7E6ErAkdD7nWG",
                    grant_type = "client_credentials"
                }).ReceiveJson<Token>();

            var buildingsResponse = await "https://datastorage-fake-myrspoven.azurewebsites.net"
                  .AppendPathSegment("Building/GetBuildings")
                  .WithOAuthBearerToken(result.access_token)
                  .PostAsync();
            var buildings = await buildingsResponse.GetJsonAsync<List<Building>>().ConfigureAwait(true);

            var signalsResponse = await "https://datastorage-fake-myrspoven.azurewebsites.net"
              .AppendPathSegment("Signal/GetSignals")
             .WithOAuthBearerToken(result.access_token)
                 .PostJsonAsync(new { buildingId = 1 });
            var singals = await signalsResponse.GetJsonAsync<List<Signal>>().ConfigureAwait(true);

            var signalsValuesResponse = await "https://datastorage-fake-myrspoven.azurewebsites.net"
  .AppendPathSegment("Signal/GetSignalValues")
    .WithOAuthBearerToken(result.access_token)
     .PostJsonAsync(new { buildingId = 1 });
            var singalValues = await signalsValuesResponse.GetJsonAsync<List<SignalData>>().ConfigureAwait(true);


            return Ok(singalValues);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}