namespace UniSystem.Core.Exceptions;

public sealed class InvalidEmailAddressException : DomainValidationException
{
    public InvalidEmailAddressException(string emailAddress)
        : base($"'{emailAddress}' is not a valid email address.")
    {
    }
}
