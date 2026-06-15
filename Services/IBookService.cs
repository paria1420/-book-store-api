using BookApi.Dtos;
using BookApi.Models;

namespace BookApi.Services;

public interface IBookService
{
    Task<PagedResult<BookResponse>> GetAll(GetBooksRequest request);
    Task<BookResponse> Create(CreateBookRequest request);
    Task<PagedResult<BookResponse>> Search(SearchBooksRequest request);
    Task<BookResponse?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<BookResponse?> UpdateAsync(int id, UpdateBookRequest request);
}