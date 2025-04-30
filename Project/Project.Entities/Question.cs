
namespace Project.Entities;

public class Question 
{
    public int QuestionId {get; set;}
    public int ExamId {get; set;}
    public Exam Exam {get; set;}
    public string QuestionText {get; set;}
    public List<string> AnswerOptions { get; set; }
    public string CorrectAnswer { get; set; }
}