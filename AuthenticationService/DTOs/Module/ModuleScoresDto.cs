using AuthenticationService.DTOs.AppUser;

namespace AuthenticationService.DTOs.Module
{
    public class ModuleScoresDto
    {
        public string Code { get; set; }
        public List<StudentScoreDto> StudentScores { get; set; }
    }
}
