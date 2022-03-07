namespace AuthenticationService.Models
{
    public class Year
    {
        public Guid Id { get; set; }
        public string StudyYear { get; set; } = "1st Year";

        // One To Many Relationship
        public List<Section>? YearSections { get; set; }
    }
}
