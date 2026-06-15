using BookApi.Dtos;
using BookApi.Models;

namespace BookApi.Services;

public interface IBookService
{
    Task<List<Book>> GetAll();
    Task<Book> Create(CreateBookRequest request);
    Task<List<Book>> Search(string? searchTerm);
    Task<Book?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Book?> UpdateAsync(int id, UpdateBookRequest request);
}