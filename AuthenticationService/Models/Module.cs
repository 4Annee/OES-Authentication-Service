namespace AuthenticationService.Models
{
    public class StudyModule
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Section> CourseStudents { get; set; }
        public ICollection<UserModel> CourseTeachers { get; set; }
    }
}
