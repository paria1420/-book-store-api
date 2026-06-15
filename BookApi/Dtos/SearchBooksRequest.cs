using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos;

public class SearchBooksRequest
{
    public string? SearchTerm { get; set; }

    [Range(0, 9999.99)]
    public decimal? MinPrice { get; set; }

    [Range(0, 9999.99)]
    public decimal? MaxPrice { get; set; }

    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}