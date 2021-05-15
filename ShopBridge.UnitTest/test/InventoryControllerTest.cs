using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.DTO;
using ShopBridge.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.UnitTest.test
{
    
    public class InventoryControllerTest
    {
        private readonly Mock<IInventoryRepository> inventoryRepository;

        public InventoryControllerTest()
        {
            inventoryRepository = new Mock<IInventoryRepository>();
        }

        [Fact]
        public async Task GetAllTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<InventoryDto>());

            var result =await inventoryController.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Add(It.IsAny<InventoryDto>())).ReturnsAsync(1);
            InventoryDto inventoryDto = new InventoryDto { Name = "Test", Description = "Description", Price = 23.5M };

            var result = await inventoryController.Post(inventoryDto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PostErrorTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Add(It.IsAny<InventoryDto>())).ReturnsAsync(0);
            InventoryDto inventoryDto = new InventoryDto { Name = "Test", Description = "Description", Price = 23.5M };

            var result = await inventoryController.Post(inventoryDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostModalErrorTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Add(It.IsAny<InventoryDto>())).ReturnsAsync(1);
            InventoryDto inventoryDto = new InventoryDto {  Description = "Description", Price = 23.5M };
            inventoryController.ModelState.AddModelError("Name", "Name is required");
            var result = await inventoryController.Post(inventoryDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Edit(It.IsAny<InventoryDto>())).ReturnsAsync(1);
            InventoryDto inventoryDto = new InventoryDto {Id=1, Name = "Test", Description = "Description", Price = 23.5M };

            var result = await inventoryController.Put(inventoryDto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutErrorTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Edit(It.IsAny<InventoryDto>())).ReturnsAsync(0);
            InventoryDto inventoryDto = new InventoryDto {Id=1, Name = "Test", Description = "Description", Price = 23.5M };

            var result = await inventoryController.Put(inventoryDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutModalErrorTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Edit(It.IsAny<InventoryDto>())).ReturnsAsync(1);
            InventoryDto inventoryDto = new InventoryDto {Id=1, Description = "Description", Price = 23.5M };
            inventoryController.ModelState.AddModelError("Name", "Name is required");
            var result = await inventoryController.Put(inventoryDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(1);           

            var result = await inventoryController.Delete(1);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteErrorTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(0);
            

            var result = await inventoryController.Delete(5);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteModalErrorTest()
        {
            InventoryController inventoryController = new InventoryController(inventoryRepository.Object);
            inventoryRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(0);
           
            inventoryController.ModelState.AddModelError("Name", "Name is required");
            var result = await inventoryController.Delete(0);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
