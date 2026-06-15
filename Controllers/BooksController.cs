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
    public async Task<IActionResult> GetBooks()
    {
        var books = await _bookService.GetAll();
        return Ok(books);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return Ok(book);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookRequest request)
    {
        var book = await _bookService.Create(request);
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string? searchTerm)
    {
        var books = await _bookService.Search(searchTerm);

        return Ok(books);
    }
}