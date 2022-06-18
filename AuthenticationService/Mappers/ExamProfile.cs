using AuthenticationService.DTOs.Exam;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<ExamCreationDto, Exam>().ReverseMap();
            CreateMap<ExamUpdateDto, Exam>().ReverseMap();
            CreateMap<ExamDtoForResult, Exam>().ReverseMap();
        }
    }
}
