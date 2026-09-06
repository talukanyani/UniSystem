using UniSystem.Core.Validation;

namespace UniSystem.Core.Models;

public sealed class Lecturer : Person
{
    private string _staffNumber = string.Empty;

    public string StaffNumber
    {
        get => _staffNumber;

        init => _staffNumber = Guard.RequiredText(
            value,
            nameof(StaffNumber),
            20
        );
    }

    public string? Title { get; set; }

    public string? Office { get; set; }

    public Guid? DepartmentId { get; set; }

    public override string ToString()
    {
        string displayName = string.IsNullOrWhiteSpace(Title)
            ? FullName
            : $"{Title} {FullName}";

        return $"{StaffNumber} - {displayName}";
    }
}
