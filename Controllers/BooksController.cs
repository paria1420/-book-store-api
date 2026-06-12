using BookApi.Dtos;
using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _bookService.GetAll();
        return Ok(books);
    }

    [HttpPost]
    public IActionResult CreateBook(CreateBookRequest request)
    {
        var book = _bookService.Create(request);
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpGet("search")]
    public IActionResult SearchBooks([FromQuery] string? searchTerm)
    {
        var books = _bookService.Search(searchTerm);

        return Ok(books);
    }
}