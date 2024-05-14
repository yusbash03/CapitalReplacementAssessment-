using CapitalReplacementTask.Models;
using CapitalReplacementTask.Repo.Question;
using CapitalReplacementTask.Repo.QuestionType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalReplacementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsTypeController : ControllerBase
    {
        private readonly IQuestionTypesRepo _questionTypes;
        public QuestionsTypeController(IQuestionTypesRepo questionTypes)
        {
            _questionTypes = questionTypes;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionTypes>>> GetAllQuestionTypess()
        {
            var questions = await _questionTypes.FindAllQuestionTypes();
            return Ok(questions);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionTypes>> CreateNewQuestionType(QuestionTypes questionTypes)
        {
            var createdType = await _questionTypes.CreateQuestionType(questionTypes);
            return Ok(createdType);
        }
    }
}
