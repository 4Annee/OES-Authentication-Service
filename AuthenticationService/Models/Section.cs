namespace AuthenticationService.Models
{
    public class Section
    {
        public Guid Id { get; set; }
        public string StudySection { get; set; } = "Section A";

        // foreign key constraints - Many To One
        public Year Year { get; set; }
        public Guid YearId { get; set; }

        // One To Many
        public List<Group> Groups { get; set; }
        public ICollection<StudyModule> StudyModules { get; set; }
    }
}
