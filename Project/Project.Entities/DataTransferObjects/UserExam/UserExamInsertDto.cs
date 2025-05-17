namespace Project.Entities.DataTransferObjects;

public record UserExamInsertDto
{
       
        public int ExamId { get; set; }
        //public int UserId {get; set;}
        public int Score { get; set; }
}