using AutoMapper;
using MyrspovenAssignment.Infrastructure;
using MyrspovenAssignment.ViewModels;

public class MappingProfile : Profile
{

    public MappingProfile()
    {

        CreateMap<SignalViewModel, Signal>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
            .ForMember(dest => dest.Rw, opt => opt.MapFrom(src => src.Rw))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Bms, opt => opt.MapFrom(src => src.Bms))
            .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId))
            .ForMember(dest => dest.Max, opt => opt.MapFrom(src => src.Max))
            .ForMember(dest => dest.Min, opt => opt.MapFrom(src => src.Min));

        CreateMap<SignalDataViewModel, SignalData>()
   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SignalId))
   .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId))
   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
   .ForMember(dest => dest.DataUtc, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DataUtc) ?
   (DateTime?)null : DateTime.Parse(src.DataUtc)))
      .ForMember(dest => dest.ReadUtc, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.ReadUtc) ?
   (DateTime?)null : DateTime.Parse(src.ReadUtc)));


        CreateMap<BuildingViewModel, Building>()
   .ForMember(dest => dest.SquaredMeters, opt => opt.MapFrom(src => src.squaredMeters))
   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
   .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
   .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Long))
   .ForMember(dest => dest.DataSetStartDate,
     opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DataSetStartDate) ?
   (DateTime?)null : DateTime.Parse(src.DataSetStartDate)))
   .ForMember(dest => dest.SignalData, opt => opt.MapFrom(src => src.SignalsData))
   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}