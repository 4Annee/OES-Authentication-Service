using AuthenticationService.DTOs.Section;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<SectionDto, Section>().ReverseMap();
            CreateMap<SectionDtoForCreation, Section>().ReverseMap();
        }
    }
}
