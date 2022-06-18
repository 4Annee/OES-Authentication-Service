using AuthenticationService.DTOs.StudentAnswer;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class StudentAnswerProfile : Profile
    {
        public StudentAnswerProfile()
        {
            CreateMap<StudentAnswer, StudentAnswerCreationDto>().ReverseMap();
            CreateMap<StudentAnswerDto, StudentAnswer>().ReverseMap();
        }
    }
}
