using AuthenticationService.Data;
using AuthenticationService.DTOs.Group;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Repositories
{
    public interface IGroupRepository
    {
        List<GroupDto> GetYearGroups(Guid YearId);
        List<GroupDto> GetSectionGroups(Guid SectionId);
        GroupDto AddGroup(GroupDtoForCreation group);
        void RemoveGroup(Guid id);
    }
    public class GroupRepository : IGroupRepository
    {
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public GroupRepository(UserServiceContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public GroupDto AddGroup(GroupDtoForCreation group)
        {
            var groupmodel = mapper.Map<Group>(group);
            groupmodel.Id = Guid.NewGuid();
            context.Add(groupmodel);
            context.SaveChanges();
            return mapper.Map<GroupDto>(groupmodel);
        }

        public List<GroupDto> GetSectionGroups(Guid SectionId)
        {
            return mapper.Map<List<GroupDto>>(context.Groups.Where(g => g.SectionId == SectionId));
        }

        public List<GroupDto> GetYearGroups(Guid YearId)
        {
            return mapper.Map<List<GroupDto>>(context.Groups.Where(g => g.YearId == YearId));
        }

        public void RemoveGroup(Guid id)
        {
            if (context.Groups.Any(e => e.Id == id))
            {
                context.Groups.Remove(context.Groups.Find(id));
                context.SaveChanges();
            }
        }
    }
}
