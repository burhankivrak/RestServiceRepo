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
    public class TrainingsessionControllerTest
    {

        private readonly Mock<ITrainingsessionRepository> mockRepo;
        private readonly TrainingsessionController trainingsessionController;

        public TrainingsessionControllerTest()
        {
            mockRepo = new Mock<ITrainingsessionRepository>();
            trainingsessionController = new TrainingsessionController(mockRepo.Object);
        }

        [Fact]
        public void GetRunningSessionDetails_ValidId_ReturnsOkResult()
        {
            var details = new List<RunningSessionDetail>
            {
                new RunningSessionDetail { Id = 1, RunningSessionId = 1, Interval_Time = 945, Interval_Speed=5.7 }
            };
            mockRepo.Setup(repo => repo.GetRunningSessionDetails(1)).Returns(details);

            var result = trainingsessionController.GetRunningSessionDetails(1);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(details, okResult.Value);
        }

        [Fact]
        public void GetRunningSessionDetails_InvalidId_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.GetRunningSessionDetails(99999)).Returns(new List<RunningSessionDetail>());

            var result = trainingsessionController.GetRunningSessionDetails(99999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetSessionsForMonthAndYear_ValidRequest_ReturnsOkResult()
        {
            var sessions = new List<object>
            {
                new RunningSession { Id = 1, Date = new DateTime(2022,1,12), MemberId = 6, Duration = 63, Avg_speed=14.1107142857143 }
            };
            mockRepo.Setup(repo => repo.GetSessionsForMonthAndYear("running", 1, 1, 2022)).Returns(sessions);

            var result = trainingsessionController.GetSessionsForMonthAndYear("running", 1, 1, 2022);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(sessions, okResult.Value);
        }

        [Fact]
        public void GetSessionsForMonthAndYear_InvalidRequest_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetSessionsForMonthAndYear("running", 1, 13, 2022))
                .Throws(new TrainingsessionException("Month must be between 1 and 12."));

            var result = trainingsessionController.GetSessionsForMonthAndYear("running", 1, 13, 2022);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequest = result.Result as BadRequestObjectResult;
            Assert.Equal("Month must be between 1 and 12.", badRequest.Value);
        }

        [Fact]
        public void GetSessionStatsForMember_ValidRequest_ReturnsOkResult()
        {
            var stats = new { TotalSessions = 1, TotalDurationInHours = 1.05, MaxSessionDuration = 63, MinSessionDuration=63, AvgSessionDuration = 63 };
            mockRepo.Setup(repo => repo.GetSessionStatsForMember("running", 1)).Returns(stats);

            var result = trainingsessionController.GetSessionStatsForMember("running", 1);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(stats, okResult.Value);
        }

        [Fact]
        public void GetSessionStatsForMember_InvalidRequest_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetSessionStatsForMember("unknown", 1))
                .Throws(new TrainingsessionException("Invalid training type specified."));

            var result = trainingsessionController.GetSessionStatsForMember("unknown", 1);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.Equal("Invalid training type specified.", badRequest.Value);
        }

        [Fact]
        public void GetSessionCountPerMonthForYear_ValidRequest_ReturnsOkResult()
        {
            var monthlyCounts = new Dictionary<int, int>
            {
                { 1, 5 },
                { 2, 3 },
                { 3, 4 }
            };
            mockRepo.Setup(repo => repo.GetSessionCountPerMonthForYear("running", 1, 2022)).Returns(monthlyCounts);

            var result = trainingsessionController.GetSessionCountPerMonthForYear("running", 1, 2022);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(monthlyCounts, okResult.Value);
        }

        [Fact]
        public void GetSessionCountPerMonthForYear_InvalidYear_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetSessionCountPerMonthForYear("running", 1, 1800))
                .Throws(new TrainingsessionException("Year is invalid."));

            var result = trainingsessionController.GetSessionCountPerMonthForYear("running", 1, 1800);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequest = result as BadRequestObjectResult;
            Assert.Equal("Year is invalid.", badRequest.Value);
        }

        [Fact]
        public void GetTrainingImpactPerMonthForYear_ValidRequest_ReturnsOkResult()
        {
            var cyclingSessions = new List<CyclingSession>
            {
                new CyclingSession
                {
                    Id = 5,
                    MemberId = 1,
                    Date = new DateTime(2021, 7, 27, 20, 48, 0),
                    Duration = 57,
                    Avg_watt = 118,
                    Max_watt = 273,
                    Avg_cadence = 50,
                    Max_cadence = 53,
                    Trainingtype = "interval",
                    Comment = null,
                    Trainingsimpact = "low"
                },
                new CyclingSession
                {
                    Id = 6,
                    MemberId = 1,
                    Date = new DateTime(2021, 4, 15, 9, 39, 0),
                    Duration = 26,
                    Avg_watt = 305,
                    Max_watt = 320,
                    Avg_cadence = 110,
                    Max_cadence = 111,
                    Trainingtype = "endurance",
                    Comment = null,
                    Trainingsimpact = "high"
                }
            };
            var trainingImpact = new List<object>
            {
                new { Month = 4, LowImpact = 0, MediumImpact = 0, HighImpact = 1 }, 
                new { Month = 7, LowImpact = 1, MediumImpact = 0, HighImpact = 0 }
            };
            mockRepo.Setup(repo => repo.GetTrainingImpactPerMonthForYear(1, 2022)).Returns(trainingImpact);

            var result = trainingsessionController.GetTrainingImpactPerMonthForYear(1, 2022);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(trainingImpact, okResult.Value);
        }

        [Fact]
        public void GetTrainingImpactPerMonthForYear_InvalidYear_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetTrainingImpactPerMonthForYear(1, 1800))
                .Throws(new TrainingsessionException("Year is invalid."));

            var result = trainingsessionController.GetTrainingImpactPerMonthForYear(1, 1800);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequest = result.Result as BadRequestObjectResult;
            Assert.Equal("Year is invalid.", badRequest.Value);
        }
    }
}
