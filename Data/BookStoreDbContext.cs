using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);
    }
}