using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos;

public class UpdateBookRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Author { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Isbn { get; set; } = string.Empty;

    [Range(0.01, 9999.99)]
    public decimal Price { get; set; }

    [Range(0, 100000)]
    public int StockQuantity { get; set; }

    public DateTime PublishedDate { get; set; }
}