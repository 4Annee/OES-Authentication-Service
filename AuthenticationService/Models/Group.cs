namespace AuthenticationService.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string StudyGroup { get; set; } = "Group 1";

        // Foreign Key Constraints - Many To One
        public Section Section { get; set;}
        public Guid SectionId { get; set; }

        // Foreign Key Constraints - Many To One
        public Year Year { get; set;}
        public Guid YearId { get; set; }

        // One To Many 
        public List<UserModel>? Students { get; set; }
    }
}
