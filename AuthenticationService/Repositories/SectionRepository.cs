using AuthenticationService.Data;
using AuthenticationService.DTOs.Section;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Repositories
{
    public interface ISectionRepository
    {
        List<SectionDto> GetYearSections(Guid YearId);
        SectionDto AddSection(SectionDtoForCreation section);
        void RemoveSection(Guid id);
    }

    public class SectionRepository : ISectionRepository
    {
        private readonly UserServiceContext context;
        private readonly IMapper mapper;

        public SectionRepository(UserServiceContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public SectionDto AddSection(SectionDtoForCreation section)
        {
            var sectionmodel = mapper.Map<Section>(section);
            sectionmodel.Id = Guid.NewGuid();
            context.Add(sectionmodel);
            context.SaveChanges();
            return mapper.Map<SectionDto>(sectionmodel);
        }

        public List<SectionDto> GetYearSections(Guid YearId)
        {
            return mapper.Map<List<SectionDto>>(context.Sections.Where(s => s.YearId == YearId));
        }

        public void RemoveSection(Guid id)
        {
            if (context.Sections.Any(e => e.Id == id))
            {
                context.Sections.Remove(context.Sections.Find(id));
                context.SaveChanges();
            }
        }
    }
}
