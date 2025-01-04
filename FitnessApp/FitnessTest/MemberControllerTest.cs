using FitnessApp.Controllers;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using FitnessDL.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTEST
{
    public class MemberControllerTest
    {

        private readonly Mock<IMemberRepository> mockRepo;
        private readonly MemberController memberController;

        public MemberControllerTest()
        {
            mockRepo = new Mock<IMemberRepository>();
            memberController = new MemberController(mockRepo.Object);
        }

        [Fact]
        public void GET_UnknownID_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetMember(99999)).Throws(new MemberException("Member doesn't exist"));

            var result = memberController.Get(99999);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnsOkResult()
        {
            Members member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres= "johan@gmail.com", Geboortedatum= new DateTime(1985, 5, 20), Verblijfsplaats="Gent",TypeKlant = KlantType.Bronze, Interesses = null };
            mockRepo.Setup(repo => repo.GetMember(1)).Returns(member);

            var result = memberController.Get(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnsMember()
        {
            Members member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres = "johan@gmail.com", Geboortedatum = new DateTime(1985, 5, 20), Verblijfsplaats = "Gent", TypeKlant = KlantType.Bronze, Interesses = null };
            mockRepo.Setup(repo => repo.GetMember(1)).Returns(member);

            var result = memberController.Get(1).Result as OkObjectResult;

            Assert.IsType<Members>(result.Value);
            Assert.Equal(1, (result.Value as Members).Id);
            Assert.Equal(member.Id, (result.Value as Members).Id);
            Assert.Equal(member.Voornaam, (result.Value as Members).Voornaam);
            Assert.Equal(member.Achternaam, (result.Value as Members).Achternaam);
            Assert.Equal(member.Emailadres, (result.Value as Members).Emailadres);
            Assert.Equal(member.Geboortedatum, (result.Value as Members).Geboortedatum);
            Assert.Equal(member.Verblijfsplaats, (result.Value as Members).Verblijfsplaats);
            Assert.Equal(member.TypeKlant, (result.Value as Members).TypeKlant);
            Assert.Equal(member.Interesses, (result.Value as Members).Interesses);
        }

        [Fact]
        public void Post_ValidMember_ReturnsCreatedAtAction()
        {
            Members member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres = "johan@gmail.com", Geboortedatum = new DateTime(1985, 5, 20), Verblijfsplaats = "Gent", TypeKlant = KlantType.Bronze, Interesses = null };

            var response = memberController.Post(member);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void Post_ExistingMember_ReturnsBadRequest()
        {
            Members member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres = "johan@gmail.com", Geboortedatum = new DateTime(1985, 5, 20), Verblijfsplaats = "Gent", TypeKlant = KlantType.Bronze, Interesses = null };
            mockRepo.Setup(repo => repo.AddMember(member)).Throws(new MemberException("Member already exists"));

            var response = memberController.Post(member);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void Put_UpdateExistingMember_ReturnsNoContent()
        {
            Members member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres = "johan@gmail.com", Geboortedatum = new DateTime(1985, 5, 20), Verblijfsplaats = "Gent", TypeKlant = KlantType.Bronze, Interesses = null };

            mockRepo.Setup(repo => repo.ExistsMember(1)).Returns(true);

            var response = memberController.Put(1, member);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void Put_AddNewMember_ReturnsCreatedAtAction()
        {
            Members member = new Members { Id = 3, Voornaam = "Pieter", Achternaam = "Saelens", Emailadres = "pieter@gmail.com", Geboortedatum = new DateTime(1978, 1, 10), Verblijfsplaats = "Lokeren", TypeKlant = KlantType.Bronze, Interesses = null };

            mockRepo.Setup(repo => repo.ExistsMember(3)).Returns(false);

            var response = memberController.Put(3, member);

            Assert.IsType<CreatedAtActionResult>(response);
        }

        [Fact]
        public void GetReservationsForMember_NoReservations_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.GetReservationsForMember(1)).Returns(new List<Reservation>());

            var response = memberController.GetReservationsForMember(1);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public void GetReservationsForMember_WithReservations_ReturnsOkResult()
        {
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, MemberId = 1, Date = DateTime.Now.AddDays(2) }
            };

            mockRepo.Setup(repo => repo.GetReservationsForMember(1)).Returns(reservations);

            var response = memberController.GetReservationsForMember(1);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GetProgramMembersForMember_NoPrograms_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.GetProgramMembersForMember(1)).Returns(new List<FitnessProgram>());

            var response = memberController.GetProgramMembersForMember(1);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public void GetProgramMembersForMember_WithPrograms_ReturnsOkResult()
        {
            var programs = new List<FitnessProgram>
            {
                new FitnessProgram { ProgramCode = "CYb1", Name = "cycling", Target = "beginner", StartDate = new DateTime(2025,1,5), MaxMembers = 25}
            };

            mockRepo.Setup(repo => repo.GetProgramMembersForMember(1)).Returns(programs);

            var response = memberController.GetProgramMembersForMember(1);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GetTrainingsessionsForMember_InvalidType_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetTrainingsessionsForMember("invalid", 1))
                    .Throws(new MemberException("Invalid training type specified."));

            var response = memberController.GetTrainingsessionsForMember("invalid", 1);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void GetTrainingsessionsForMember_ValidType_ReturnsOkResult()
        {
            var sessions = new List<object>
            {
                new RunningSession{ Id = 1, Date =new DateTime(2024,12,16), MemberId = 1, Duration= 63, Avg_speed= 13.653}
            };

            mockRepo.Setup(repo => repo.GetTrainingsessionsForMember("running", 1)).Returns(sessions);

            var response = memberController.GetTrainingsessionsForMember("running", 1);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GetTrainingsessionsForMember_NoType_ReturnsAllSessions()
        {
            var sessions = new List<object>
            {
                new RunningSession{ Id = 1, Date =new DateTime(2024,12,16), MemberId = 1, Duration= 63, Avg_speed= 13.653},
                new CyclingSession { Id = 1, Date =new DateTime(2024,12,19), Duration= 63, Avg_watt = 203, Max_watt=313, Avg_cadence=82, Max_cadence=90, Trainingtype="fun", Comment= null, MemberId=1}
            };

            mockRepo.Setup(repo => repo.GetTrainingsessionsForMember(null, 1)).Returns(sessions);

            var response = memberController.GetTrainingsessionsForMember(null, 1);

            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
