using CapitalReplacementTask.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CapitalReplacementTask.Repo.Question
{
    public class QuestionsRepo : IQuestionsRepo
    {
        private readonly CosmosClient _client;
        private readonly IConfiguration _configuration;
        private readonly Container _container;

        public QuestionsRepo(CosmosClient client, IConfiguration configuration) 
        {
            _client = client;
            _configuration = configuration;
            var _containerName = "Questions";
            var dbName = configuration["CosmosDBSettings:DBName"];
            _container = client.GetContainer(dbName, _containerName);
        }

        public async Task<Questions> CreateQuestion(Questions question)
        {
            var itemAdd = await _container.CreateItemAsync(question);
            return itemAdd.Resource;
        }

        public async Task<IEnumerable<Questions>> FindAllQuestions()
        {
            var query = _container.GetItemLinqQueryable<Questions>()
                .Where(x => x.isDeleted == false).ToFeedIterator();
            var allquestions = new List<Questions>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                allquestions.AddRange(response);
            }

            return allquestions;
        }

        public async Task<IEnumerable<Questions>> FindAllQuestionsByTypeId(string Id)
        {
            var query = _container.GetItemLinqQueryable<Questions>()
            .Where(x => x.QuestionTypeId == Id && x.isDeleted == false).ToFeedIterator();
            var allquestions = new List<Questions>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                allquestions.AddRange(response);
            }

            return allquestions;
        }

        public async Task<Questions> FindCandidateQuestionById(string Id, string userId)
        {
            var query = _container.GetItemLinqQueryable<Questions>()
           .Where(x => x.id == Id && x.isDeleted == false && x.userId == userId)
           .Take(1)
           .ToFeedIterator();

            var response = await query.ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<Questions> FindQuestionById(string Id)
        {
            var query = _container.GetItemLinqQueryable<Questions>()
          .Where(x => x.id == Id && x.isDeleted == false)
          .Take(1)
          .ToFeedIterator();

            var response = await query.ReadNextAsync();
            return response.FirstOrDefault();
        }

        public async Task<Questions> UpdateQuestion(Questions question)
        {
            var response = await _container.ReplaceItemAsync(question, question.id);
            return response.Resource;
        }
    }
}
