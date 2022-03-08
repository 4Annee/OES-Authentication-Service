using AuthenticationService.DTOs.Group;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupDto, Group>().ReverseMap();
            CreateMap<GroupDtoForCreation, Group>().ReverseMap();
        }
    }
}
