
namespace Project.Entities.DataTransferObjects;

public record UserExamDto
{
        public int UserExamId { get; set; }

        public int UserId { get; set; }
       
        public int ExamId { get; set; }

        public int Score { get; set; }
}