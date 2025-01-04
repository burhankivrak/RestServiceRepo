using FitnessApp.Controllers;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using FitnessDL.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FitnessTEST
{
    public class EquipmentControllerTest
    {
        private readonly Mock<IEquipmentRepository> mockRepo;
        private readonly EquipmentController equipmentController;

        public EquipmentControllerTest()
        {
            mockRepo = new Mock<IEquipmentRepository>();
            equipmentController = new EquipmentController(mockRepo.Object);
        }

        [Fact]
        public void GET_UnknownID_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.GetEquipment(99)).Throws(new EquipmentException("Equipment doesn't exist"));
            var result = equipmentController.Get(99);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnsOkResult()
        {
            Equipment equipment = new Equipment { Id = 1, Name = "Treadmill", Status = Status.available };

            mockRepo.Setup(repo => repo.GetEquipment(1)).Returns(equipment);

            var result = equipmentController.Get(1);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GET_CorrectID_ReturnsEquipment()
        {
            Equipment e = new Equipment { Id = 1, Name = "Treadmill", Status = Status.available };

            mockRepo.Setup(repo => repo.GetEquipment(1)).Returns(e);
            var result = equipmentController.Get(1).Result as OkObjectResult;
            Assert.IsType<Equipment>(result.Value);
            Assert.Equal(1, (result.Value as Equipment).Id);
            Assert.Equal(e.Name, (result.Value as Equipment).Name);
            Assert.Equal(e.Status, (result.Value as Equipment).Status);
            Assert.Equal(e.Id, (result.Value as Equipment).Id );
        }

        [Fact]
        public void Post_ValidEquipment_ReturnsCreatedAtAction()
        {
            Equipment equipment = new Equipment { Id = 2, Name = "Bike", Status = Status.available };

            var response = equipmentController.Post(equipment);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void Post_ValidEquipment_ReturnsCorrectItem()
        {
            Equipment equipment = new Equipment { Id = 2, Name = "Bike", Status = Status.available };
            mockRepo.Setup(repo => repo.AddEquipment(It.IsAny<Equipment>()));

            var response = equipmentController.Post(equipment);
            var createdAtActionResult = response.Result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);

            var item = createdAtActionResult.Value as Equipment;

            Assert.IsType<Equipment>(item);
            Assert.Equal(equipment.Id, item.Id);
            Assert.Equal(equipment.Name, item.Name);
            Assert.Equal(equipment.Status, item.Status);
        }

        [Fact]
        public void Post_ExistingEquipment_ReturnsBadRequest()
        {
            Equipment equipment = new Equipment { Id = 2, Name = "Bike", Status = Status.available };
            mockRepo.Setup(repo => repo.AddEquipment(It.IsAny<Equipment>()))
                        .Throws(new EquipmentException("Equipment already exists"));
            var response = equipmentController.Post(equipment).Result;

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void Put_UpdateValidEquipmentStatus_ReturnsNoContent()
        {
            mockRepo.Setup(repo => repo.UpdateEquipmentStatus(1, Status.maintanance));

            var result = equipmentController.Put(1, Status.maintanance);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_UpdateInvalidEquipmentStatus_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.UpdateEquipmentStatus(1, Status.maintanance))
                    .Throws(new EquipmentException("Equipment doesn't exist"));

            var result = equipmentController.Put(1, Status.maintanance);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_ValidEquipment_ReturnsNoContent()
        {
            mockRepo.Setup(repo => repo.ExistsEquipment(1)).Returns(true);

            var result = equipmentController.Remove(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_UnknownEquipment_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.ExistsEquipment(99)).Returns(false);

            var result = equipmentController.Remove(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_EquipmentWithFutureReservations_ReturnsBadRequest()
        {
            mockRepo.Setup(repo => repo.ExistsEquipment(1)).Returns(true);
            mockRepo.Setup(repo => repo.RemoveEquipment(1))
                    .Throws(new EquipmentException("Equipment cannot be deleted because it has active future reservations"));

            var result = equipmentController.Remove(1);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}