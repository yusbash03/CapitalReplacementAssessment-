using CapitalReplacementTask.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Configuration;

namespace CapitalReplacementTask.Repo.QuestionType
{
    public class QuestionTypesRepo : IQuestionTypesRepo
    {
        private readonly CosmosClient _client;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        public QuestionTypesRepo(CosmosClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            var _containerName = "QuestionsType";
            var dbName = configuration["CosmosDBSettings:DBName"];
            _container = client.GetContainer(dbName, _containerName);
        }
        public async Task<QuestionTypes> CreateQuestionType(QuestionTypes questionTypes)
        {
            questionTypes.id = Guid.NewGuid().ToString();
            var itemAdd = await _container.CreateItemAsync(questionTypes);
            return itemAdd.Resource;
        }

        public async Task<IEnumerable<QuestionTypes>> FindAllQuestionTypes()
        {
            var query = _container.GetItemLinqQueryable<QuestionTypes>()
               .Where(x => x.isDeleted == false).ToFeedIterator();
            var allquestions = new List<QuestionTypes>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                allquestions.AddRange(response);
            }

            return allquestions;
        }

        public async Task<QuestionTypes> FindQuestionTypeById(string Id)
        {
            var query = _container.GetItemLinqQueryable<QuestionTypes>()
         .Where(x => x.id == Id && x.isDeleted == false)
         .Take(1)
         .ToFeedIterator();

            var response = await query.ReadNextAsync();
            return response.FirstOrDefault();
        }
    }
}
