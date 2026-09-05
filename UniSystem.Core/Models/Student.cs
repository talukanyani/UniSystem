using UniSystem.Core.Enums;

namespace UniSystem.Core.Models;

public sealed class Student : Person
{
    public required string StudentNumber { get; init; }
    public DateOnly RegistrationDate { get; init; }
        = DateOnly.FromDateTime(DateTime.Now);
    public StudentStatus Status { get; init; } = StudentStatus.Active;

    public override string ToString()
    {
        return $"{StudentNumber} - {base.ToString()} [{Status}]";
    }
}
