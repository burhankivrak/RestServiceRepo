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
    public class ReservationTimeslotControllerTest
    {
        private readonly Mock<IReservationTimeslotRepository> mockRepo;
        private readonly ReservationTimeslotController reservationTimeslotController;

        public ReservationTimeslotControllerTest()
        {
            mockRepo = new Mock<IReservationTimeslotRepository>();
            reservationTimeslotController = new ReservationTimeslotController(mockRepo.Object);
        }

        [Fact]
        public void Post_ValidReservationTimeslot_ReturnsCreatedAtAction()
        {
            ReservationTimeslot reservationTimeslot = new ReservationTimeslot
            {
                ReservationTimeslotId = 1,
                EquipmentId = 1,
                ReservationId = 1,
                TimeslotId = 1
            };

            mockRepo.Setup(repo => repo.AddReservationTimeslot(reservationTimeslot));

            var result = reservationTimeslotController.Post(reservationTimeslot);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.Equal("Get", actionResult.ActionName);
            Assert.Equal(reservationTimeslot.ReservationTimeslotId, actionResult.RouteValues["id"]);
        }

        [Fact]
        public void Post_ReservationTimeslotAlreadyExists_ReturnsBadRequest()
        {
            ReservationTimeslot reservationTimeslot = new ReservationTimeslot
            {
                ReservationTimeslotId = 1,
                EquipmentId = 1,
                ReservationId = 1,
                TimeslotId = 1
            };

            mockRepo.Setup(repo => repo.AddReservationTimeslot(reservationTimeslot))
                .Throws(new ReservationException("Reservation already exists"));

            var result = reservationTimeslotController.Post(reservationTimeslot);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal("Reservation already exists", badRequestResult.Value);
        }

        [Fact]
        public void Get_ValidId_ReturnsOkResult()
        {
            ReservationTimeslot reservationTimeslot = new ReservationTimeslot
            {
                ReservationTimeslotId = 1,
                EquipmentId = 1,
                ReservationId = 1,
                TimeslotId = 1
            };

            mockRepo.Setup(repo => repo.GetReservationTimeslot(1)).Returns(reservationTimeslot);

            var result = reservationTimeslotController.Get(1);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(reservationTimeslot, okResult.Value);
        }

        [Fact]
        public void Get_InvalidId_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.GetReservationTimeslot(99999))
                .Throws(new ReservationException("Reservation doesn't exist"));

            var result = reservationTimeslotController.Get(99999);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal("Reservation doesn't exist", badRequestResult.Value);
        }

        [Fact]
        public void Put_ValidReservationTimeslot_ReturnsNoContent()
        {
            ReservationTimeslot reservationTimeslot = new ReservationTimeslot
            {
                ReservationTimeslotId = 1,
                EquipmentId = 1,
                ReservationId = 1,
                TimeslotId = 1
            };

            mockRepo.Setup(repo => repo.ExistsReservationTimeslot(1)).Returns(true);
            mockRepo.Setup(repo => repo.UpdateReservationTimeslot(reservationTimeslot));

            var result = reservationTimeslotController.Put(1, reservationTimeslot);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_ReservationTimeslotDoesNotExist_ReturnsCreatedAtAction()
        {
            ReservationTimeslot reservationTimeslot = new ReservationTimeslot
            {
                ReservationTimeslotId = 1,
                EquipmentId = 1,
                ReservationId = 1,
                TimeslotId = 1
            };

            mockRepo.Setup(repo => repo.ExistsReservationTimeslot(1)).Returns(false);
            mockRepo.Setup(repo => repo.AddReservationTimeslot(reservationTimeslot));

            var result = reservationTimeslotController.Put(1, reservationTimeslot);

            Assert.IsType<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(reservationTimeslot.ReservationTimeslotId, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public void Put_InvalidReservationTimeslot_ReturnsBadRequest()
        {
            ReservationTimeslot reservationTimeslot = null;

            var result = reservationTimeslotController.Put(1, reservationTimeslot);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
