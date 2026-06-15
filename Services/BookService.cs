using BookApi.Data;
using BookApi.Dtos;
using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Services;

public class BookService : IBookService
{
    private readonly BookStoreDbContext _context;

    public BookService(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookResponse>> GetAll(GetBooksRequest request)
    {

        return await _context.Books
            .AsNoTracking()
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(book => new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                PublishedDate = book.PublishedDate
            })
            .ToListAsync();
    }

    public async Task<BookResponse> Create(CreateBookRequest request)
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

        return MapToResponse(book);
    }

    public async Task<List<BookResponse>> Search(string? searchTerm)
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

        return await query
            .Select(book => new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                PublishedDate = book.PublishedDate
            })
            .ToListAsync();
    }

    public async Task<BookResponse?> GetByIdAsync(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return book is null ? null : MapToResponse(book);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return false;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request)
    {
        var book = await _context.Books.FindAsync(id);

        if (book is null)
        {
            return null;
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.Isbn = request.Isbn;
        book.Price = request.Price;
        book.StockQuantity = request.StockQuantity;
        book.PublishedDate = request.PublishedDate;

        await _context.SaveChangesAsync();

        return MapToResponse(book);
    }

    private static BookResponse MapToResponse(Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Price = book.Price,
            StockQuantity = book.StockQuantity,
            PublishedDate = book.PublishedDate
        };
    }
}