using BookApi.Controllers;
using BookApi.Dtos;
using BookApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace BookApi.Tests;

[TestFixture]
public class BooksControllerTests
{
    private Mock<IBookService> _bookServiceMock = null!;
    private BooksController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _bookServiceMock = new Mock<IBookService>();
        _controller = new BooksController(_bookServiceMock.Object);
    }

    [Test]
    public async Task GetBooks_WhenBooksExist_ReturnsOkWithPagedResult()
    {
        // Arrange
        var request = new GetBooksRequest
        {
            PageNumber = 1,
            PageSize = 10
        };

        var pagedResult = new PagedResult<BookResponse>
        {
            Items =
            [
                new BookResponse
                {
                    Id = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Isbn = "9780132350884",
                    Price = 29.99m,
                    StockQuantity = 5,
                    PublishedDate = new DateTime(2008, 8, 1)
                }
            ],
            PageNumber = 1,
            PageSize = 10,
            TotalCount = 1
        };

        _bookServiceMock
            .Setup(x => x.GetAllAsync(It.IsAny<GetBooksRequest>()))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.GetBooks(request);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var response = okResult.Value.Should().BeOfType<PagedResult<BookResponse>>().Subject;

        response.Items.Should().HaveCount(1);
        response.Items[0].Title.Should().Be("Clean Code");
        response.TotalCount.Should().Be(1);
    }
}
