using AuthenticationService.DTOs.Year;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class YearProfile : Profile
    {
        public YearProfile()
        {
            CreateMap<YearDto, Year>().ReverseMap();
            CreateMap<YearDtoForCreation, Year>().ReverseMap();
        }

    }
}
