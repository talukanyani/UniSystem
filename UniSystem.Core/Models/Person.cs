namespace UniSystem.Core.Models;

public abstract class Person
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public ContactDetails? ContactDetails { get; init; }

    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        String email = ContactDetails?.Email ?? "No email";
        return $"{FullName} ({email})";
    }
}
