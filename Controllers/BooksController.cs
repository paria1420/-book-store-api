using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = new[]
        {
            new { Id = 1, Title = "Clean Code", Author = "Robert C. Martin" },
            new { Id = 2, Title = "Atomic Habits", Author = "James Clear" }
        };

        return Ok(books);
    }
}