using AuthenticationService.Data;
using AuthenticationService.DTOs.Year;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Repositories
{
    public interface IYearRepository
    {
        List<YearDto> GetAllYears();
        Task<YearDto> AddYear(YearDtoForCreation year);
        Task RemoveYear(Guid id);
    }

    public class YearRepository : IYearRepository
    {
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public YearRepository(UserServiceContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<YearDto> AddYear(YearDtoForCreation year)
        {
            var yearmodel = mapper.Map<Year>(year);
            yearmodel.Id = Guid.NewGuid();
            await context.AddAsync(yearmodel);
            context.SaveChanges();
            return mapper.Map<YearDto>(yearmodel);
        }

        public List<YearDto> GetAllYears()
        {
            return mapper.Map<List<YearDto>>(context.Years.ToList());
        }

        public Task RemoveYear(Guid id)
        {
            if(context.Years.Any(e => e.Id == id))
            {
                context.Years.Remove(context.Years.Find(id));
                context.SaveChanges();
                return Task.CompletedTask;
            }
            return Task.FromResult(false);
        }
    }
}
