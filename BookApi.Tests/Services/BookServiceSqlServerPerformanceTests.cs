using BookApi.Data;
using BookApi.Dtos;
using BookApi.Models;
using BookApi.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace BookApi.Tests.Services;

[TestFixture]
[Category("Performance")]
public class BookServiceSqlServerPerformanceTests
{
    private MsSqlContainer _sqlContainer = null!;
    private BookStoreDbContext _context = null!;
    private BookService _bookService = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _sqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

        await _sqlContainer.StartAsync();
    }

    [SetUp]
    public async Task SetUp()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseSqlServer(_sqlContainer.GetConnectionString())
            .Options;

        _context = new BookStoreDbContext(options);

        await _context.Database.MigrateAsync();

        _context.Books.RemoveRange(_context.Books);
        await _context.SaveChangesAsync();

        var books = Enumerable.Range(1, 1000)
            .Select(i => new Book
            {
                Title = i % 10 == 0 ? $"Clean Code Volume {i}" : $"Book Title {i}",
                Author = i % 5 == 0 ? "Robert C. Martin" : $"Author {i}",
                Isbn = $"ISBN-{i}",
                Price = 10 + i,
                StockQuantity = i,
                PublishedDate = new DateTime(2020, 1, 1).AddDays(i)
            })
            .ToList();

        await _context.Books.AddRangeAsync(books);
        await _context.SaveChangesAsync();

        _bookService = new BookService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _sqlContainer.DisposeAsync();
    }

    [Test]
    public async Task SearchAsync_With1000Books_UsesRealSqlServerAndReturnsResultsQuickly()
    {
        var request = new SearchBooksRequest
        {
            SearchTerm = "Clean",
            PageNumber = 1,
            PageSize = 20
        };

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var result = await _bookService.SearchAsync(request);

        stopwatch.Stop();

        result.TotalCount.Should().Be(100);
        result.Items.Should().HaveCount(20);
        result.Items.Should().OnlyContain(x => x.Title.Contains("Clean"));

        stopwatch.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
}