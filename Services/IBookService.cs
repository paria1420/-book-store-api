using BookApi.Dtos;
using BookApi.Models;

namespace BookApi.Services;

public interface IBookService
{
    Task<List<BookResponse>> GetAll(GetBooksRequest request);
    Task<BookResponse> Create(CreateBookRequest request);
    Task<List<BookResponse>> Search(string? searchTerm);
    Task<BookResponse?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request);
}