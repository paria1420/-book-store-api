namespace BookApi.Dtos;

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Isbn { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public DateTime PublishedDate { get; set; }
}