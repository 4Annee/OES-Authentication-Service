using AuthenticationService.DTOs.Exam;
using AuthenticationService.Models;
using AutoMapper;

namespace AuthenticationService.Mappers
{
    public class StudentPassedExamProfile : Profile
    {
        public StudentPassedExamProfile()
        {
            CreateMap<StudentPassedExam, ExamResultDto>()
                .ForMember(dest => dest.AssessmentTitle, opt => opt.MapFrom(o => o.Exam.AssessmentTitle))
                .ForMember(dest => dest.PassingDate, opt => opt.MapFrom(o => o.Exam.StartTime));
            CreateMap<StudentPassedExam, StudentResult>().
                ForMember(dest => dest.FullName, opt => opt.MapFrom(o => o.Student.FullName));
        }
    }
}
