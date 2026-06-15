namespace BookApi.Exceptions;

public class DuplicateBookIsbnException : Exception
{
    public DuplicateBookIsbnException(string isbn)
        : base($"A book with ISBN '{isbn}' already exists.")
    {
    }
}