using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopBridge.DTO;
using ShopBridge.Middlewares;
using ShopBridge.Models;
using ShopBridge.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.UnitTest.test
{
    public class InventoryRepositoryTest
    {
        private Mock<IMapper> mapper;
        private Mock<InventoryDBContext> context;

        public InventoryRepositoryTest()
        {
            mapper = new Mock<IMapper>();
            context = new Mock<InventoryDBContext>();
        }

        [Fact]
        public async Task GetAllTest()
        {
            mapper.Setup(x => x.Map<List<InventoryDto>>(It.IsAny<List<Inventory>>())).Returns(new List<InventoryDto>()
              { new InventoryDto { Id = 1, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M } });

            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new InventoryDBContext(options))
            {
                context.Inventories.Add(new Inventory { Id = 1, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M });                
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new InventoryDBContext(options))
            {
                InventoryRepository inventoryRepository = new InventoryRepository(context,mapper.Object);
                IEnumerable<InventoryDto> inventories = await inventoryRepository.GetAll();
    
                Assert.Single(inventories);
            }
        }

        [Fact]
        public async Task AddTestOk()
        {
            mapper.Setup(x => x.Map<Inventory>(It.IsAny<InventoryDto>())).Returns(new Inventory() {  Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M });
            var invetoryDto = new InventoryDto() { Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M };
            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;
                      
            using (var context = new InventoryDBContext(options))
            {
                InventoryRepository inventoryRepository = new InventoryRepository(context, mapper.Object);
                int success = await inventoryRepository.Add(invetoryDto);

                Assert.Equal(success, 1);
            }
        }

        [Fact]
        public async Task AddTestFailed()
        {

            InventoryDto invetoryDto = null;
            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;

            using (var context = new InventoryDBContext(options))
            {
                InventoryRepository inventoryRepository = new InventoryRepository(context, mapper.Object);
                int success = await inventoryRepository.Add(invetoryDto);

                Assert.Equal(success, 0);
            }
        }

        [Fact]
        public async Task DeleteIfRecordExistTest()
        {
            mapper.Setup(x => x.Map<List<InventoryDto>>(It.IsAny<List<Inventory>>())).Returns(new List<InventoryDto>()
              { new InventoryDto { Id = 1, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M } });

            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new InventoryDBContext(options))
            {
                context.Inventories.Add(new Inventory { Id = 3, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new InventoryDBContext(options))
            {
                InventoryRepository inventoryRepository = new InventoryRepository(context, mapper.Object);
                int success = await inventoryRepository.Delete(3);

                Assert.Equal(success, 1);
            }
        }

        [Fact]
        
        public async Task DeleteIfRecordNotExistTest()
        {
            mapper.Setup(x => x.Map<List<InventoryDto>>(It.IsAny<List<Inventory>>())).Returns(new List<InventoryDto>()
              { new InventoryDto { Id = 7, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M } });

            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new InventoryDBContext(options))
            {
                context.Inventories.Add(new Inventory { Id = 6, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new InventoryDBContext(options))
            {
                InventoryRepository inventoryRepository = new InventoryRepository(context, mapper.Object);               

                await Assert.ThrowsAsync<NotFoundException>(() => inventoryRepository.Delete(7));
            }
        }

        [Fact]
        public async Task EditIfRecordExistTest()
        {
            var invetoryDto = new InventoryDto() {Id=5, Name = "Inventory Edited", Description = "Inventory test description", Price = 25.4M };
            mapper.Setup(x => x.Map<Inventory>(It.IsAny<InventoryDto>())).Returns(new Inventory
               { Id = 1, Name = "Inventory Edited", Description = "Inventory test description", Price = 25.4M });

            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new InventoryDBContext(options))
            {
                context.Inventories.Add(new Inventory { Id = 5, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M });
                context.SaveChanges();

                InventoryRepository inventoryRepository = new InventoryRepository(context, mapper.Object);
                int success = await inventoryRepository.Edit(invetoryDto);

                Assert.Equal(success, 1);
            }            
           
        }

        [Fact]

        public async Task EditIfRecordNotExistTest()
        {
            var invetoryDto = new InventoryDto() { Id = 4, Name = "Inventory Edited", Description = "Inventory test description", Price = 25.4M };
            mapper.Setup(x => x.Map<List<InventoryDto>>(It.IsAny<List<Inventory>>())).Returns(new List<InventoryDto>()
              { new InventoryDto { Id = 1, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M } });

            var options = new DbContextOptionsBuilder<InventoryDBContext>().UseInMemoryDatabase(databaseName: "InventoryDatabase").Options;
            // Insert seed data into the database using one instance of the context
            using (var context = new InventoryDBContext(options))
            {
                context.Inventories.Add(new Inventory { Id = 9, Name = "Inventory 1", Description = "Inventory test description", Price = 25.4M });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new InventoryDBContext(options))
            {
                InventoryRepository inventoryRepository = new InventoryRepository(context, mapper.Object);               

                await Assert.ThrowsAsync<NotFoundException>(() => inventoryRepository.Edit(invetoryDto));
            }
        }
    }
}
