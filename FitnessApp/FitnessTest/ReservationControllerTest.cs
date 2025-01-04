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
    public class ReservationControllerTest
    {
        private readonly Mock<IReservationRepository> mockRepo;
        private readonly ReservationController reservationController;

        public ReservationControllerTest()
        {
            mockRepo = new Mock<IReservationRepository>();
            reservationController = new ReservationController(mockRepo.Object);
        }

        [Fact]
        public void GET_UnknownReservationID_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetReservation(99999)).Throws(new ReservationException("Reservation doesn't exist"));

            var result = reservationController.Get(99999);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectReservationID_ReturnsOkResult()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                MemberId = 1,
                Date = DateTime.Now.AddDays(1),
                Member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres = "johan@gmail.com", Geboortedatum = new DateTime(1985, 5, 20), Verblijfsplaats = "Gent", TypeKlant = KlantType.Bronze, Interesses = null }
            };

            mockRepo.Setup(repo => repo.GetReservation(1)).Returns(reservation);

            var result = reservationController.Get(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectReservationID_ReturnsCorrectReservation()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                MemberId = 1,
                Date = DateTime.Now.AddDays(1),
                Member = new Members { Id = 1, Voornaam = "Johan", Achternaam = "Saelens", Emailadres = "johan@gmail.com", Geboortedatum = new DateTime(1985, 5, 20), Verblijfsplaats = "Gent", TypeKlant = KlantType.Bronze, Interesses = null }
            };

            mockRepo.Setup(repo => repo.GetReservation(1)).Returns(reservation);

            var result = reservationController.Get(1).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsType<Reservation>(result.Value);
            Assert.Equal(1, ((Reservation)result.Value).Id);
        }

        [Fact]
        public void POST_ValidReservation_ReturnsCreatedAtAction()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                MemberId = 1,
                Date = DateTime.Now.AddDays(2)
            };

            mockRepo.Setup(repo => repo.AddReservation(It.IsAny<Reservation>()));

            var result = reservationController.Post(reservation);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void POST_InvalidReservation_ReturnsBadRequest()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                MemberId = 1,
                Date = DateTime.Now.AddDays(-1) 
            };

            mockRepo.Setup(repo => repo.AddReservation(It.IsAny<Reservation>()))
                    .Throws(new ReservationException("Reservation date must be in the future."));

            var result = reservationController.Post(reservation).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PUT_UpdateValidReservation_ReturnsNoContent()
        {
            Reservation reservation = new Reservation
            {
                Id = 1,
                MemberId = 1,
                Date = DateTime.Now.AddDays(3)
            };

            mockRepo.Setup(repo => repo.ExistsReservation(1)).Returns(true);
            mockRepo.Setup(repo => repo.UpdateReservation(It.IsAny<Reservation>()));

            var result = reservationController.Put(1, reservation);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PUT_AddNewReservation_ReturnsCreatedAtAction()
        {
            Reservation reservation = new Reservation
            {
                Id = 2,
                MemberId = 1,
                Date = DateTime.Now.AddDays(4)
            };

            mockRepo.Setup(repo => repo.ExistsReservation(2)).Returns(false);
            mockRepo.Setup(repo => repo.AddReservation(It.IsAny<Reservation>()));

            var result = reservationController.Put(2, reservation);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void DELETE_ValidReservation_ReturnsNoContent()
        {
            mockRepo.Setup(repo => repo.ExistsReservation(1)).Returns(true);
            mockRepo.Setup(repo => repo.RemoveReservation(1));

            var result = reservationController.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DELETE_UnknownReservation_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.ExistsReservation(99999)).Returns(false);

            var result = reservationController.Delete(99999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DELETE_ReservationWithException_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.ExistsReservation(1)).Returns(true);
            mockRepo.Setup(repo => repo.RemoveReservation(1))
                    .Throws(new ReservationException("Cannot delete reservation."));

            var result = reservationController.Delete(1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
