using BookApi.Dtos;
using BookApi.Models;

namespace BookApi.Services;

public interface IBookService
{
    List<Book> GetAll();

    Book Create(CreateBookRequest request);

    List<Book> Search(string? searchTerm);
}