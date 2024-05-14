using CapitalReplacementTask.Models;

namespace CapitalReplacementTask.Repo.QuestionType
{
    public interface IQuestionTypesRepo
    {
        Task<QuestionTypes> FindQuestionTypeById(string Id);

        Task<IEnumerable<QuestionTypes>> FindAllQuestionTypes();
        Task<QuestionTypes> CreateQuestionType(QuestionTypes questionTypes);
    }
}
