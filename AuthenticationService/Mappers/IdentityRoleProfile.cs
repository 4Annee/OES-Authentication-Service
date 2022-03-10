using AuthenticationService.DTOs.IdentityRole;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Mappers
{
    public class IdentityRoleProfile : Profile
    {
        public IdentityRoleProfile()
        {
            CreateMap<IdentityRole, IdentityRoleDto>().ReverseMap();
            CreateMap<IdentityRoleDtoForCreation, IdentityRole>().ForMember(r=>r.NormalizedName,opt=>opt.MapFrom(re=>re.Name)).ReverseMap();
        }
    }
}
