namespace UniSystem.Core.Models;

public sealed class Lecturer : Person
{
    public required string StaffNumber { get; init; }
    public string? Title { get; init; }
    public string? Office { get; init; }
    public Guid? DepartmentId { get; init; }
    public override string ToString()
    {
        string displayName 
            = string.IsNullOrWhiteSpace(Title) ? FullName : $"{Title} {FullName}";
        return $"{StaffNumber} - {displayName}";
    }
}
