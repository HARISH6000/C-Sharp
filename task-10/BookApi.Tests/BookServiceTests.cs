using BookApi.Data;
using BookApi.Models;
using BookApi.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookApi.Tests
{
    public class BookServiceTests
    {
        private BookContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new BookContext(options);
        }

        [Fact]
        public async Task CreateAsync_AddsBook()
        {
            var context = GetInMemoryDbContext();
            var service = new BookService(context);
            var book = new Book { Title = "Test Book", Author = "Test Author", Price = 10.99m };

            var result = await service.CreateAsync(book);

            Assert.NotEqual(0, result.Id);
            Assert.Equal(1, await context.Books.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsBook()
        {
            var context = GetInMemoryDbContext();
            var service = new BookService(context);
            var book = new Book { Title = "Test Book", Author = "Test Author", Price = 10.99m };
            await service.CreateAsync(book);

            var result = await service.GetByIdAsync(book.Id);

            Assert.NotNull(result);
            Assert.Equal(book.Title, result.Title);
        }
    }
}