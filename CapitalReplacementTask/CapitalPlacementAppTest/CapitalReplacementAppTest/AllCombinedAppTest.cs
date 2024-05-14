using CapitalReplacementTask.Controllers;
using CapitalReplacementTask.Models;
using CapitalReplacementTask.Models.DTOs;
using CapitalReplacementTask.Repo.Candidate;
using CapitalReplacementTask.Repo.Question;
using CapitalReplacementTask.Repo.QuestionType;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementAppTest.CapitalReplacementAppTest
{
    public class AllCombinedAppTest
    {
        private readonly Mock<IQuestionsRepo> _questionsRepoMock;
        private readonly Mock<IQuestionTypesRepo> _questionsTypeRepoMock;
        private readonly Mock<ICandidatesRepo> _candidateRepoMock;
        private readonly QuestionsController _controllerQ;
        private readonly QuestionsTypeController _controllerQType;
        private readonly CandidateController _controllerCandidate;
        string typeId = "qt1";

        public AllCombinedAppTest()
        {
            _questionsRepoMock = new Mock<IQuestionsRepo>();
            _questionsTypeRepoMock = new Mock<IQuestionTypesRepo>();
            _candidateRepoMock = new Mock<ICandidatesRepo>();

            _controllerQ = new QuestionsController(_questionsRepoMock.Object);
            _controllerQType = new QuestionsTypeController(_questionsTypeRepoMock.Object);
            _controllerCandidate = new CandidateController(_candidateRepoMock.Object);
        }


        [Fact]
        public async Task CreateNewQuestionType_Test()
        {
            // Arrange 
            var expected = new QuestionTypes
            {
                id = "q1",
                nameOfType = "Number",
                isDeleted = false
            };

            _questionsTypeRepoMock.Setup(
                service => service.CreateQuestionType(It.IsAny<QuestionTypes>())
            ).ReturnsAsync(expected);

            // Act
            var result = await _controllerQType.CreateNewQuestionType(expected);

            // Assert
            Assert.NotNull(result);
            
        }


        [Fact]
        public async Task UpdateQuestion_Test()
        {
            string QID = "q1";
            string userId = "user1";
            // Arrange 
            var expected = new Questions
            {
                id = "q1",
                Question = "Number",
                QuestionTypeId = "qt1",
                isDeleted = false
            };

            _questionsRepoMock.Setup(
                service => service.UpdateQuestion(It.IsAny<Questions>())
            ).ReturnsAsync(expected);

            // Act
            var result = await _controllerQ.UpdateQuestion(QID, userId,expected);

            // Assert
            Assert.NotNull(result);
           
        }

        [Fact]
        public async Task GetAllQuestionsByQuestionTypeId_Test()
        {
            var expectedQuestions = new List<Questions>
        {
            new Questions { id = "q1", Question = "Question 1", QuestionTypeId = typeId },
            new Questions { id = "q2", Question = "Question 2", QuestionTypeId = typeId }
        };

            _questionsRepoMock.Setup(repo => repo.FindAllQuestionsByTypeId(typeId))
                             .ReturnsAsync(expectedQuestions);

            // Act
            var actionResult = await _controllerQ.GetAllQuestionsByTypeId(typeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualQuestions = Assert.IsAssignableFrom<IEnumerable<Questions>>(okResult.Value);
            Assert.Equal(expectedQuestions, actualQuestions);
        }

        [Fact]
        public async Task ApplicantSubmission_Test()
        {
            string QID = "q1";
            string userId = "user1";
            // Arrange 
            var expected = new CandidateDTO
            {
                id = "c1",
            firstName = "Eric",
            lastName = "Larson",
            email= "eLars@hotmail.com",
            phone= "009900",
            nationality= "Nigerian",
            residence= "Ikeja",
            idNumber= "AD112",
            dateOfBirth= "2003-04-13T12:00:00Z",
            gender= "Male",
            answers= ["Paragraph message", false, 23, "2003-04-13T12:00:00Z"]
            };

            _candidateRepoMock.Setup(
                service => service.CreateNewApplicant(It.IsAny<CandidateDTO>())
            ).ReturnsAsync(expected);

            // Act
            var result = await _controllerCandidate.CreateNewApplicant(expected);

            // Assert
            Assert.NotNull(result);

        }
    }
}
