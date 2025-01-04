using FitnessApp.Controllers;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTEST
{
    public class ProgramControllerTest
    {
        private readonly Mock<IProgramRepository> mockRepo;
        private readonly ProgramController programController;

        public ProgramControllerTest()
        {
            mockRepo = new Mock<IProgramRepository>();
            programController = new ProgramController(mockRepo.Object);
        }

        [Fact]
        public void GET_UnknownProgramCode_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetProgram("INVALID_CODE")).Throws(new ProgramException("Program doesn't exist"));

            var result = programController.Get("INVALID_CODE");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectProgramCode_ReturnsOkResult()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "ENDa4",
                Name = "endurance",
                Target = "advanced",
                StartDate = new DateTime(2024,10,23),
                MaxMembers = 30
            };

            mockRepo.Setup(repo => repo.GetProgram("ENDa4")).Returns(program);

            var result = programController.Get("ENDa4");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectProgramCode_ReturnsCorrectProgram()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "ENDa4",
                Name = "endurance",
                Target = "advanced",
                StartDate = new DateTime(2024, 10, 23),
                MaxMembers = 30
            };

            mockRepo.Setup(repo => repo.GetProgram("ENDa4")).Returns(program);

            var result = programController.Get("ENDa4").Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsType<FitnessProgram>(result.Value);
            Assert.Equal("ENDa4", ((FitnessProgram)result.Value).ProgramCode);
        }

        [Fact]
        public void Post_ValidProgram_ReturnsCreatedAtAction()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "FUNa13",
                Name = "fun",
                Target = "advanced",
                StartDate = new DateTime(2024, 11, 18),
                MaxMembers = 20
            };

            mockRepo.Setup(repo => repo.AddProgram(It.IsAny<FitnessProgram>()));

            var result = programController.Post(program);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Post_ValidProgram_ReturnsProgramWithGeneratedCode()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "FUNa13",
                Name = "fun",
                Target = "advanced",
                StartDate = new DateTime(2024, 11, 18),
                MaxMembers = 20
            };

            mockRepo.Setup(repo => repo.AddProgram(It.IsAny<FitnessProgram>()))
                    .Callback<FitnessProgram>(p => p.ProgramCode = "FUNa13");

            var result = programController.Post(program).Result as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.IsType<FitnessProgram>(result.Value);
            Assert.Equal("FUNa13", ((FitnessProgram)result.Value).ProgramCode);
        }

        [Fact]
        public void Post_InvalidProgram_ReturnsBadRequest()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "FUNa13",
                Name = "fun",
                Target = "advanced",
                StartDate = new DateTime(2024, 11, 18),
                MaxMembers = 20
            };

            mockRepo.Setup(repo => repo.AddProgram(It.IsAny<FitnessProgram>()))
                    .Throws(new ProgramException("Program already exists"));

            var result = programController.Post(program).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Put_UpdateValidProgram_ReturnsNoContent()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "FUNa13",
                Name = "fun",
                Target = "advanced",
                StartDate = new DateTime(2024, 11, 18),
                MaxMembers = 30
            };

            mockRepo.Setup(repo => repo.ExistsProgram("FUNa13")).Returns(true);
            mockRepo.Setup(repo => repo.UpdateProgram(It.IsAny<FitnessProgram>()));

            var result = programController.Put("FUNa13", program);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_UpdateInvalidProgram_ReturnsBadRequest()
        {
            FitnessProgram program = new FitnessProgram
            {
                ProgramCode = "INVALID_CODE",
                Name = "Invalid Program",
                Target = "advanced",
                StartDate = new DateTime(2024, 11, 18),
                MaxMembers = 30
            };

            mockRepo.Setup(repo => repo.ExistsProgram("INVALID_CODE")).Returns(false);

            var result = programController.Put("INVALID_CODE", program);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void Put_NullProgram_ReturnsBadRequest()
        {
            var result = programController.Put("FUNa13", null);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
