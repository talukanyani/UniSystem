using System.Net.Mail;
using UniSystem.Core.Exceptions;

namespace UniSystem.Core.Models;

public sealed record ContactDetails
{
    public string Email { get; }

    public string? PhoneNumber { get; }

    public ContactDetails(string email, string? phoneNumber)
    {
        string normalizedEmail = email?.Trim() ?? string.Empty;

        bool isValidEmail =
            MailAddress.TryCreate(
                normalizedEmail,
                out MailAddress? address
            )
            && string.Equals(
                address.Address,
                normalizedEmail,
                StringComparison.OrdinalIgnoreCase
            );

        if (!isValidEmail)
        {
            throw new InvalidEmailAddressException(email!);
        }

        Email = normalizedEmail;

        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber)
            ? null
            : phoneNumber.Trim();
    }
}
