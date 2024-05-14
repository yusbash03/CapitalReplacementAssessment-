using CapitalReplacementTask.Models;
using CapitalReplacementTask.Models.DTOs;

namespace CapitalReplacementTask.Repo.Candidate
{
    public interface ICandidatesRepo
    {
        Task<Candidates> FindQuestionTypeById(string Id);

        Task<IEnumerable<Candidates>> FindAllApplicants();
        Task<CandidateDTO> CreateNewApplicant(CandidateDTO candidateDTO);
        Task<Answers> CreateNewAnsweres(Answers answers);

    }
}
