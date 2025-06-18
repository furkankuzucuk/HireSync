
namespace Project.Entities.DataTransferObjects;

public record UserExamDto
{
        public int UserExamId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public int ExamId { get; set; }
        public string ExamName {get; set;}
        public int Score { get; set; }
}