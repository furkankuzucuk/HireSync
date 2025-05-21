using AutoMapper;
using Project.Entities;
using Project.Entities.DataTransferObjects;
using Project.Entities.DataTransferObjects.Department;
using Project.Entities.DataTransferObjects.Candidate;
using Project.Entities.DataTransferObjects.Exam;
using Project.Entities.DataTransferObjects.Job;
using Project.Entities.DataTransferObjects.JobApplication;
using Project.Entities.DataTransferObjects.JobList;
using Project.Entities.DataTransferObjects.LeaveRequest;
using Project.Entities.DataTransferObjects.Login;
using Project.Entities.DataTransferObjects.PerformanceReview;
using Project.Entities.DataTransferObjects.SatisfactionSurvey;
using Project.Entities.DataTransferObjects.SurveyAnswer;
using Project.Entities.DataTransferObjects.User;

namespace Project.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // USER
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDtoInsertion, User>().ReverseMap();
            CreateMap<UserDtoCandidateInsert, User>().ReverseMap();
            CreateMap<UserDtoUpdate, User>().ReverseMap();

            // LOGIN
            CreateMap<Login, LoginDto>().ReverseMap();
            CreateMap<LoginDtoInsertion, Login>().ReverseMap();
            CreateMap<LoginDtoUpdate, Login>().ReverseMap();

            // JOB
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<JobInsertDto, Job>().ReverseMap();
            CreateMap<JobUpdateDto, Job>().ReverseMap();

            // DEPARTMENT
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<DepartmentInsertDto, Department>().ReverseMap();
            CreateMap<DepartmentUpdateDto, Department>().ReverseMap();

            // JOB APPLICATION
<<<<<<< Updated upstream
            CreateMap<JobApplication, JobApplicationDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.JobList.Title))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.JobList.Department.DepartmentName))
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.JobList.Job.JobName))
                .ForMember(dest => dest.JobList, opt => opt.MapFrom(src => src.JobList))
                .ReverseMap();

=======
            CreateMap<JobApplication, JobApplicationDto>().ReverseMap();
>>>>>>> Stashed changes
            CreateMap<JobApplicationInsertDto, JobApplication>().ReverseMap();
            CreateMap<JobApplicationUpdateDto, JobApplication>().ReverseMap();

            // JOB LIST
            CreateMap<JobList, JobListDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
<<<<<<< Updated upstream
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.JobName))
=======
                .ForMember(dest => dest.JobName, opt => opt.MapFrom(src => src.Job.JobName)) // Job entity bağlıysa JobName al
>>>>>>> Stashed changes
                .ReverseMap();

            CreateMap<JobListInsertDto, JobList>().ReverseMap();
            CreateMap<JobListUpdateDto, JobList>().ReverseMap();

            // LEAVE REQUEST
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequestInsertDto, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestUpdateDto, LeaveRequest>().ReverseMap();

            // PERFORMANCE REVIEW
            CreateMap<PerformanceReview, PerformanceReviewDto>().ReverseMap();
            CreateMap<PerformanceReviewInsertDto, PerformanceReview>().ReverseMap();
            CreateMap<PerformanceReviewUpdateDto, PerformanceReview>().ReverseMap();

            // SATISFACTION SURVEY
            CreateMap<SatisfactionSurvey, SatisfactionSurveyDto>().ReverseMap();
            CreateMap<SatisfactionSurveyInsertDto, SatisfactionSurvey>().ReverseMap();
            CreateMap<SatisfactionSurveyUpdateDto, SatisfactionSurvey>().ReverseMap();

<<<<<<< Updated upstream
            // SURVEY ANSWER
            CreateMap<SurveyAnswer, SurveyAnswerDto>().ReverseMap();
            CreateMap<SurveyAnswerInsertDto, SurveyAnswer>().ReverseMap();
            CreateMap<SurveyAnswerUpdateDto, SurveyAnswer>().ReverseMap();

            // SURVEY QUESTION
            CreateMap<SurveyQuestion, SurveyQuestionDto>().ReverseMap();
            CreateMap<SurveyQuestionInsertDto, SurveyQuestion>().ReverseMap();
            CreateMap<SurveyQuestionUpdateDto, SurveyQuestion>().ReverseMap();

=======
>>>>>>> Stashed changes
            // EXAM
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<ExamInsertDto, Exam>().ReverseMap();
            CreateMap<ExamUpdateDto, Exam>().ReverseMap();

            // QUESTION
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<QuestionInsertDto, Question>().ReverseMap();
            CreateMap<QuestionUpdateDto, Question>().ReverseMap();

<<<<<<< Updated upstream
            // USER EXAM
            CreateMap<UserExam, UserExamDto>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.ExamName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap();

=======
            // CANDIDATE
            CreateMap<Candidate, CandidateDto>().ReverseMap();
            CreateMap<CandidateDtoInsertion, Candidate>().ReverseMap();
            CreateMap<CandidateDtoUpdate, Candidate>().ReverseMap();

            // USER EXAM
            CreateMap<UserExam, UserExamDto>().ReverseMap();
>>>>>>> Stashed changes
            CreateMap<UserExamInsertDto, UserExam>().ReverseMap();
            CreateMap<UserExamUpdateDto, UserExam>().ReverseMap();
        }
    }
}
