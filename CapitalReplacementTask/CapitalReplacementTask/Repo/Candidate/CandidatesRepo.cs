using CapitalReplacementTask.Models;
using CapitalReplacementTask.Models.DTOs;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CapitalReplacementTask.Repo.Candidate
{
    public class CandidatesRepo : ICandidatesRepo
    {
        private readonly CosmosClient _client;
        private readonly IConfiguration _configuration;
        private readonly Container _container;

        public CandidatesRepo(CosmosClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            var _containerName = "CandidateProfile";
            var dbName = configuration["CosmosDBSettings:DBName"];
            _container = client.GetContainer(dbName, _containerName);
        }

        public async Task<Answers> CreateNewAnsweres(Answers answers)
        {
            answers.Id = Guid.NewGuid().ToString();
            var itemAdd = await _container.CreateItemAsync(answers);
            return itemAdd.Resource;
        }

        public async Task<CandidateDTO> CreateNewApplicant(CandidateDTO candidateDTO)
        {
            candidateDTO.id = Guid.NewGuid().ToString();
            var itemAdd = await _container.CreateItemAsync(candidateDTO);
           
            var result = new CandidateDTO
            {
                id = itemAdd.Resource.id,
                firstName = itemAdd.Resource.firstName,
                lastName = itemAdd.Resource.lastName,
                idNumber = itemAdd.Resource.idNumber,
                phone = itemAdd.Resource.phone,
                email = itemAdd.Resource.email,
                nationality = itemAdd.Resource.nationality,
                residence = itemAdd.Resource.residence,
                gender = itemAdd.Resource.gender,
            };
            return result;
        }

        public async Task<IEnumerable<Candidates>> FindAllApplicants()
        {
            var query = _container.GetItemLinqQueryable<Candidates>()
               .Where(x => x.isDeleted == false).ToFeedIterator();
            var allquestions = new List<Candidates>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                allquestions.AddRange(response);
            }

            return allquestions;
        }

        public async Task<Candidates> FindQuestionTypeById(string Id)
        {
            var query = _container.GetItemLinqQueryable<Candidates>()
         .Where(x => x.id == Id && x.isDeleted == false)
         .Take(1)
         .ToFeedIterator();

            var response = await query.ReadNextAsync();
            return response.FirstOrDefault();
        }
    }
}
