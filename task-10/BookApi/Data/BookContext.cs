using Microsoft.EntityFrameworkCore;

namespace BookApi.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Models.Book> Books { get; set; }
    }
}