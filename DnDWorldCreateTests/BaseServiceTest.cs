using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services;
using DnDWorldCreate.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DnDWorldCreateTests
{
    public class TestEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class BaseServiceTests
    {
        private readonly DbContextOptions<TestDnDWorldContext> _options;

        public BaseServiceTests()
        {
            _options = new DbContextOptionsBuilder<TestDnDWorldContext>()
            .UseInMemoryDatabase(databaseName: "DnDWorldCreate_Test_BaseService")
            .Options;
        }

        private BaseService<TestEntity> GetBaseService()
        {
            var dbContextFactory = new TestDbContextFactory(_options);
            var repository = new EfRepository<TestEntity>(dbContextFactory);
            var service = new BaseService<TestEntity>(repository, dbContextFactory);
            return service;
        }


        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var service = GetBaseService();
            var newEntity = new TestEntity { Name = "TestEntity", Description = "TestDescription" };

            // Act
            await service.AddAsync(newEntity);
            await service.SaveChangesAsync();

            // Assert
            using (var context = new TestDnDWorldContext(_options))
            {
                var entity = context.Set<TestEntity>().FirstOrDefault(e => e.Name == newEntity.Name);
                Assert.NotNull(entity);
                Assert.Equal(newEntity.Name, entity.Name);
                Assert.Equal(newEntity.Description, entity.Description);
            }
        }
        [Fact]
        public async Task AddAsync_ShouldThrowArgumentException()
        {
            // Arrange
            var service = GetBaseService();

            // Act & Assert

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddAsync(null));
        }
        [Fact]
        public async Task DeleteAsync_ShouldDeleteRegion()
        {
            // Arrange
            var service = GetBaseService();
            var newRegion = new TestEntity { Name = "TestRegion", Description = "TestDescription" };
            await service.AddAsync(newRegion);
            await service.SaveChangesAsync();

            // Act
            await service.DeleteAsync(newRegion.Id);
            await service.SaveChangesAsync();

            // Assert
            using (var context = new TestDnDWorldContext(_options))
            {
                var region = context.Regions.FirstOrDefault(r => r.Id == newRegion.Id);
                Assert.Null(region);
            }
        }
        [Fact]
        public async Task DeleteRegionAsync_ShouldThrowErrorWhenRegionIsNull()
        {
            // Arrange
            var service = GetBaseService();
            int nonExistentRegionId = -1; // Or any value that you know doesn't correspond to an existing region

            // Act && Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(nonExistentRegionId));
        }
        [Fact]
        public async Task DeleteRegionAsync_ShouldThrowErrorWhenRegionIdDoesNotExist()
        {
            // Arrange
            var service = GetBaseService();
            Region newRegion = new Region { Id = 0 };

            // Act && Assert

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(newRegion.Id));

        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateRegion()
        {
            // Arrange
            var service = GetBaseService();
            var originalEntity = new TestEntity { Name = "OriginalRegion", Description = "OriginalDescription" };

            // Act
            await service.AddAsync(originalEntity);
            await service.SaveChangesAsync();

            var originalDescription = originalEntity.Description;
            var updatedEntity = new TestEntity { Name = originalEntity.Name, Description = "New Update", Id = originalEntity.Id };
            await service.UpdateAsync(updatedEntity);
            await service.SaveChangesAsync();

            // Assert
            var retrievedRegion = await service.GetByIdAsync(originalEntity.Id);
            Assert.NotNull(retrievedRegion);
            Assert.NotEqual(originalDescription, retrievedRegion.Description);
            Assert.Equal(updatedEntity.Description, retrievedRegion.Description);
        }
        // ... other test methods
    }
}
