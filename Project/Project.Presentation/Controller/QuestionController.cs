
using Microsoft.AspNetCore.Mvc;
using Project.Services.Concretes;
using Project.Services.Contracts;

namespace Project.Presentation.Controller;

public class QuestionController : ControllerBase
{
    private readonly IServiceManager serviceManager;

    public QuestionController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var reviews = await serviceManager.QuestionService.GetAllQuestions(false);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var review = await serviceManager.QuestionService.GetQuestionById(id,false);
            return Ok(review);
        }

        

}