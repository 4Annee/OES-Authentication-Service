using AuthenticationService.Data;
using AuthenticationService.DTOs.AppUser;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repositories
{
    public interface IUserRepository
    {
        public List<AppUserDto> SearchForUser(string SearchTerm);
        public AppUserDto GetUserDetails(string Userid);
        public List<AppUserDto> GetStudentsByGroup(Guid GroupId);
        public List<AppUserDto> GetStudentsBySection(Guid SectionId);
        public List<AppUserDto> GetStudentsByYear(Guid YearId);
        public List<AppUserDto> GetTeachersList();
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public UserRepository(UserServiceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<AppUserDto> GetStudentsByGroup(Guid GroupId)
        {
            return mapper.Map<List<AppUserDto>>(
                context.Users.Where(u => u.GroupId == GroupId).ToList());
        }

        public List<AppUserDto> GetStudentsBySection(Guid SectionId)
        {
            return mapper.Map<List<AppUserDto>>(
                context.Users.Include(u=>u.Group).Where(u => u.Group != null && u.Group.SectionId == SectionId).ToList());
        }

        public List<AppUserDto> GetStudentsByYear(Guid YearId)
        {
            return mapper.Map<List<AppUserDto>>(
               context.Users.Include(u => u.Group).Where(u => u.Group != null && u.Group.YearId == YearId).ToList());
        }

        public List<AppUserDto> GetTeachersList()
        {
            var teacherroleid = context.Roles.Where(r => r.NormalizedName == "Teacher").FirstOrDefault().Id;
            var teachers = context.UserRoles.Where(ur=>ur.RoleId == teacherroleid).Select(ur=>ur.UserId).ToList();

            return mapper.Map<List<AppUserDto>>(
                context.Users.Where(u => teachers.Contains(u.Id)).ToList());
        }

        public AppUserDto GetUserDetails(string Userid)
        {
            return mapper.Map<AppUserDto>(context.Users.Find(Userid));
        }

        public List<AppUserDto> SearchForUser(string SearchTerm)
        {
            return mapper.Map<List<AppUserDto>>(context.Users
                .Where(u=>u.FullName.ToLower().Contains(SearchTerm.ToLower()) 
                    || u.Email.ToLower().Contains(SearchTerm.ToLower()))
            );
        }
    }
}
