using CapitalReplacementTask.Models;
using CapitalReplacementTask.Models.DTOs;
using CapitalReplacementTask.Repo.Candidate;
using CapitalReplacementTask.Repo.Question;
using CapitalReplacementTask.Repo.QuestionType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace CapitalReplacementTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidatesRepo _candidates;
        public CandidateController( ICandidatesRepo candidates)
        {
            _candidates = candidates;
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateNewApplicant(CandidateDTO candidateDTO)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var createdCandidate = await _candidates.CreateNewApplicant(candidateDTO);
                    string candidateId = createdCandidate.id;
                    foreach (var answer in candidateDTO.answers)
                    {
                        var ans = new Answers();
                        ans.Answer = answer;
                        ans.isDeleted = false;
                        ans.CandidateId = candidateId;
                        await _candidates.CreateNewAnsweres(ans);
                    }

                    // If everything succeeded, commit the transaction
                    scope.Complete();

                    return Ok(createdCandidate);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            //var createdType = await _candidates.CreateNewApplicant(candidateDTO);

            //return Ok(createdType);
        }
    }
}
