using Microsoft.EntityFrameworkCore;
using Presentation.Contexts;
using Presentation.Models;
using Presentation.Service;


namespace Presentation.Tests
{
    public class ArtistServiceTests
    {
        private async Task<AppDbContext> GetDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }

        [Fact]
        public async Task AddAsync_ShouldAddArtist()
        {
            var db = await GetDbContextAsync();
            var service = new ArtistService(db);

            var artist = new Artists { Id = Guid.NewGuid().ToString(), Name = "Test Artist", Description = "Test Desc" };
            await service.AddAsync(artist);

            var result = await db.Artists.FindAsync(artist.Id);
            Assert.NotNull(result);
            Assert.Equal("Test Artist", result!.Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllArtists()
        {
            var db = await GetDbContextAsync();
            db.Artists.AddRange(
                new Artists { Id = "1", Name = "Artist 1", Description = "Desc 1" },
                new Artists { Id = "2", Name = "Artist 2", Description = "Desc 2" }
            );
            await db.SaveChangesAsync();

            var service = new ArtistService(db);
            var result = await service.GetAllAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectArtist()
        {
            var db = await GetDbContextAsync();
            var artist = new Artists { Id = "123", Name = "Solo", Description = "Unique" };
            db.Artists.Add(artist);
            await db.SaveChangesAsync();

            var service = new ArtistService(db);
            var result = await service.GetByIdAsync("123");

            Assert.NotNull(result);
            Assert.Equal("Solo", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingArtist()
        {
            var db = await GetDbContextAsync();
            var artist = new Artists { Id = "a1", Name = "Old Name", Description = "Old Desc" };
            db.Artists.Add(artist);
            await db.SaveChangesAsync();

            var service = new ArtistService(db);
            var updated = new Artists { Name = "New Name", Description = "New Desc" };

            var success = await service.UpdateAsync("a1", updated);
            var result = await db.Artists.FindAsync("a1");

            Assert.True(success);
            Assert.Equal("New Name", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalseIfArtistNotFound()
        {
            var db = await GetDbContextAsync();
            var service = new ArtistService(db);

            var result = await service.UpdateAsync("nonexistent", new Artists());

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveArtist()
        {
            var db = await GetDbContextAsync();
            var artist = new Artists { Id = "toDelete", Name = "To Delete", Description = "Gone" };
            db.Artists.Add(artist);
            await db.SaveChangesAsync();

            var service = new ArtistService(db);
            var success = await service.DeleteAsync("toDelete");

            var result = await db.Artists.FindAsync("toDelete");

            Assert.True(success);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalseIfNotFound()
        {
            var db = await GetDbContextAsync();
            var service = new ArtistService(db);

            var result = await service.DeleteAsync("invalid");

            Assert.False(result);
        }
    }
}
