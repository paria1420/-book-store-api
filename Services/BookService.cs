using BookApi.Dtos;
using BookApi.Models;

namespace BookApi.Services;

public class BookService : IBookService
{
    private static readonly List<Book> Books =
    [
        new Book
        {
            Id = 1,
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Isbn = "9780132350884",
            Price = 29.99m,
            StockQuantity = 10,
            PublishedDate = new DateTime(2008, 8, 1)
        }
    ];

    public List<Book> GetAll()
    {
        return Books;
    }

    public Book Create(CreateBookRequest request)
    {
        var book = new Book
        {
            Id = Books.Count + 1,
            Title = request.Title,
            Author = request.Author,
            Isbn = request.Isbn,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            PublishedDate = request.PublishedDate
        };

        Books.Add(book);

        return book;
    }

    public List<Book> Search(string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Books;
        }

        return Books
            .Where(x =>
                x.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.Isbn.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}