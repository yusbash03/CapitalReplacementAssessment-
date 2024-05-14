using CapitalReplacementTask.Models;
using CapitalReplacementTask.Models.DTOs;

namespace CapitalReplacementTask.Repo.Question
{
    public interface IQuestionsRepo
    {

        Task<Questions> FindQuestionById(string Id);

        Task<IEnumerable<Questions>> FindAllQuestions();
        Task<IEnumerable<Questions>> FindAllQuestionsByTypeId(string Id);
        Task<Questions> FindCandidateQuestionById(string Id, string userId);
        Task<Questions> CreateQuestion(Questions question);
        Task<Questions> UpdateQuestion(Questions question);
    }
}
