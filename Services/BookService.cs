using BookApi.Data;
using BookApi.Dtos;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Services;

public class BookService : IBookService
{private readonly BookStoreDbContext _context;

    public BookService(BookStoreDbContext context)
    {
        _context = context;
    }

    public async  Task<List<Book>> GetAll()
    {
        return await _context.Books
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Book> Create(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Isbn = request.Isbn,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            PublishedDate = request.PublishedDate
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async  Task<List<Book>> Search(string? searchTerm)
    {
        var query = _context.Books
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x =>
                x.Title.Contains(searchTerm) ||
                x.Author.Contains(searchTerm) ||
                x.Isbn.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }
}