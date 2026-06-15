using BookApi.Dtos;
using BookApi.Models;

namespace BookApi.Services;

public interface IBookService
{
    Task<PagedResult<BookResponse>> GetAllAsync(GetBooksRequest request);
    Task<BookResponse> CreateAsync(CreateBookRequest request);
    Task<PagedResult<BookResponse>> SearchAsync(SearchBooksRequest request);
    Task<BookResponse?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request);
    Task<int> SeedRandomBooksAsync(int count);
}