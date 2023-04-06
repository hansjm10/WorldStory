using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services;
using DnDWorldCreate.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DnDWorldCreateTests
{
    public class RegionServiceTests
    {
        private readonly DbContextOptions<DnDWorldContext> _options;

        public RegionServiceTests()
        {
            _options = new DbContextOptionsBuilder<DnDWorldContext>()
                .UseInMemoryDatabase(databaseName: "DnDWorldCreate_Test")
                .Options;
        }

        private RegionService GetRegionService()
        {
            var dbContextFactory = new TestDbContextFactory(_options);
            var repository = new EfRepository<Region>(dbContextFactory);
            var service = new RegionService(repository, dbContextFactory);
            return service;
        }
        [Fact]
        public async Task AddRegionAsync_ShouldAddRegion()
        {
            // Arrange
            var service = GetRegionService();
            var newRegion = new Region { Name = "TestRegion", Description = "TestDescription" };

            // Act
            await service.AddRegionAsync(newRegion);
            await service.SaveChangesAsync();

            // Assert
            using (var context = new DnDWorldContext(_options))
            {
                var region = context.Regions.FirstOrDefault(r => r.Name == newRegion.Name);
                Assert.NotNull(region);
                Assert.Equal(newRegion.Name, region.Name);
                Assert.Equal(newRegion.Description, region.Description);
            }
        }
        [Fact]
        public async Task AddRegionAsync_ShouldThrowArgumentException()
        {
            // Arrange
            var service = GetRegionService();

            // Act & Assert

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddRegionAsync(null));
        }
        [Fact]
        public async Task DeleteRegionAsync_ShouldDeleteRegion()
        {
            // Arrange
            var service = GetRegionService();
            var newRegion = new Region { Name = "TestRegion", Description = "TestDescription" };
            await service.AddRegionAsync(newRegion);
            await service.SaveChangesAsync();

            // Act
            await service.DeleteRegionAsync(newRegion.Id);
            await service.SaveChangesAsync();

            // Assert
            using (var context = new DnDWorldContext(_options))
            {
                var region = context.Regions.FirstOrDefault(r => r.Id == newRegion.Id);
                Assert.Null(region);
            }
        }
        [Fact]
        public async Task DeleteRegionAsync_ShouldThrowErrorWhenRegionIsNull()
        {
            // Arrange
            var service = GetRegionService();
            int nonExistentRegionId = -1; // Or any value that you know doesn't correspond to an existing region

            // Act && Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteRegionAsync(nonExistentRegionId));
        }
        [Fact]
        public async Task DeleteRegionAsync_ShouldThrowErrorWhenRegionIdDoesNotExist()
        {
            // Arrange
            var service = GetRegionService();
            Region newRegion = new Region { Id = 0 };

            // Act && Assert

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteRegionAsync(newRegion.Id));

        }
        [Fact]
        public async Task UpdateRegionAsync_ShouldUpdateRegion()
        {
            // Arrange
            var service = GetRegionService();
            var originalRegion = new Region { Name = "OriginalRegion", Description = "OriginalDescription" };

            // Act
            await service.AddRegionAsync(originalRegion);
            await service.SaveChangesAsync();

            var originalDescription = originalRegion.Description;
            var updatedRegion = new Region { Name = originalRegion.Name, Description = "New Update", Id = originalRegion.Id };
            await service.UpdateRegionAsync(updatedRegion);
            await service.SaveChangesAsync();

            // Assert
            var retrievedRegion = await service.GetRegionByIdAsync(originalRegion.Id);
            Assert.NotNull(retrievedRegion);
            Assert.NotEqual(originalDescription, retrievedRegion.Description);
            Assert.Equal(updatedRegion.Description, retrievedRegion.Description);
        }

    }
}
