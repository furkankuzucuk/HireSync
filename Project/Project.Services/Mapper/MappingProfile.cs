using AutoMapper;
using Project.Entities;
using Project.Entities.DataTransferObjects;
using Project.Entities.DataTransferObjects.Candidate;
using Project.Entities.DataTransferObjects.Department;
using Project.Entities.DataTransferObjects.Exam;
using Project.Entities.DataTransferObjects.Job;
using Project.Entities.DataTransferObjects.JobApplication;
using Project.Entities.DataTransferObjects.JobList;
using Project.Entities.DataTransferObjects.LeaveRequest;
using Project.Entities.DataTransferObjects.Login;
using Project.Entities.DataTransferObjects.PerformanceReview;
using Project.Entities.DataTransferObjects.SatisfactionSurvey;
using Project.Entities.DataTransferObjects.User;

namespace Project.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDtoInsertion, User>().ReverseMap();
            CreateMap<UserDtoCandidateInsert, User>().ReverseMap();
            CreateMap<UserDtoUpdate, User>().ReverseMap();

            // Login
            CreateMap<Login, LoginDto>().ReverseMap();
            CreateMap<LoginDtoInsertion, Login>().ReverseMap();
            CreateMap<LoginDtoUpdate, Login>().ReverseMap();

            // Job
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<JobInsertDto, Job>().ReverseMap();
            CreateMap<JobUpdateDto, Job>().ReverseMap();

            // Department
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<DepartmentInsertDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();

            // Job Application
            CreateMap<JobApplication, JobApplicationDto>().ReverseMap();
            CreateMap<JobApplicationInsertDto, JobApplication>().ReverseMap();
            CreateMap<JobApplicationUpdateDto, JobApplication>().ReverseMap();

            // JobList (sadece Insert ve Update için)
            CreateMap<JobListInsertDto, JobList>().ReverseMap();
            CreateMap<JobListUpdateDto, JobList>().ReverseMap();

            // Leave Request
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequestInsertDto, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestUpdateDto, LeaveRequest>().ReverseMap();

            // Performance Review
            CreateMap<PerformanceReview, PerformanceReviewDto>().ReverseMap();
            CreateMap<PerformanceReviewInsertDto, PerformanceReview>().ReverseMap();
            CreateMap<PerformanceReviewUpdateDto, PerformanceReview>().ReverseMap();

            // Satisfaction Survey
            CreateMap<SatisfactionSurvey, SatisfactionSurveyDto>().ReverseMap();
            CreateMap<SatisfactionSurveyInsertDto, SatisfactionSurvey>().ReverseMap();
            CreateMap<SatisfactionSurveyUpdateDto, SatisfactionSurvey>().ReverseMap();

            // Exam
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<ExamInsertDto, Exam>().ReverseMap();
            CreateMap<ExamUpdateDto, Exam>().ReverseMap();

            // Candidate
            CreateMap<Candidate, CandidateDto>().ReverseMap();
            CreateMap<CandidateDtoInsertion, Candidate>().ReverseMap();
            CreateMap<CandidateDtoUpdate, Candidate>().ReverseMap();

            // User Exam
            CreateMap<UserExam, UserExamDto>().ReverseMap();
            CreateMap<UserExamInsertDto, UserExam>().ReverseMap();
            CreateMap<UserExamUpdateDto, UserExam>().ReverseMap();

            CreateMap<Question,QuestionDto>().ReverseMap();
            CreateMap<QuestionInsertDto,Question>().ReverseMap();
            CreateMap<QuestionUpdateDto,Question>().ReverseMap();
        }
    }
}
