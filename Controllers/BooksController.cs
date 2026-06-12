using BookApi.Dtos;
using BookApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class BooksController : ControllerBase
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
    [HttpGet]
    public IActionResult GetBooks()
    {
        return Ok(Books);
    }

    [HttpPost]
    public IActionResult CreateBook(CreateBookRequest request)
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

        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }
}