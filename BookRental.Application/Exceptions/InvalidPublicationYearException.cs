namespace BookRental.Application.Exceptions;

public class InvalidPublicationYearException : Exception
{
    public InvalidPublicationYearException(string message)
        : base(message)
    {
    }
}
