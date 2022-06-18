using AuthenticationService.DTOs.QuestionSolution;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class QuestionSolutionProfile : Profile
    {
        public QuestionSolutionProfile()
        {
            CreateMap<QuestionSolution, SolutionCreationDto>().ReverseMap();
        }
    }
}
