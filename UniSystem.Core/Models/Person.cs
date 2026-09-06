using UniSystem.Core.Validation;

namespace UniSystem.Core.Models;

public abstract class Person
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string FirstName
    {
        get => _firstName;

        init => _firstName = Guard.RequiredText(
            value,
            nameof(FirstName),
            50
        );
    }

    public string LastName
    {
        get => _lastName;

        init => _lastName = Guard.RequiredText(
            value,
            nameof(LastName),
            50
        );
    }

    public ContactDetails? ContactDetails { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        string email = ContactDetails?.Email ?? "No email";
        return $"{FullName} ({email})";
    }
}
