using CapitalReplacementTask.Models;
using CapitalReplacementTask.Repo.Question;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalReplacementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsRepo _questionsRepo;
        public QuestionsController(IQuestionsRepo questionsRepo) 
        {
            _questionsRepo = questionsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetAllQuestions()
        {
            var questions = await _questionsRepo.FindAllQuestions();
            return Ok(questions);
        }

        //[HttpPost]
        //public async Task<ActionResult<Questions>> CreateNewQuestion()
        //{
        //    var questions = await _questionsRepo.FindAllQuestions();
        //    return Ok(questions);
        //}

        [HttpPut("UpdateQuestion/{id}/{userId}")]
        public async Task<ActionResult<Questions>> UpdateQuestion(string id, string userId, Questions questions)
        {
            var existingQuestion = await _questionsRepo.FindCandidateQuestionById(id, userId);
            //var existingQuestion = await _questionsRepo.FindQuestionById(id);
            if (existingQuestion == null) 
            {
                return NotFound();
            }

            questions.id = existingQuestion.id;
            var updatedQuestion = await _questionsRepo.UpdateQuestion(questions);

            return Ok(updatedQuestion);
        }

        [HttpGet("GetAllQuestionsByTypeId/{id}")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetAllQuestionsByTypeId(string id)
        {
            var questions = await _questionsRepo.FindAllQuestionsByTypeId(id);
            return Ok(questions);
        }
    }
}
