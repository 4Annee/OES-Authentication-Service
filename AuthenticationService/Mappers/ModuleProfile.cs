using AuthenticationService.DTOs.Module;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<ModuleDtoForCreation, StudyModule>()
                .ForMember(e => e.Id, opt => opt.MapFrom(s => s.Code)).ReverseMap();
            CreateMap<ModuleDto, StudyModule>()
                .ForMember(e => e.Id, opt => opt.MapFrom(s => s.Code)).ReverseMap();

        }
    }
}
