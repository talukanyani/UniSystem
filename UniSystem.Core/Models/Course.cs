using UniSystem.Core.Enums;

namespace UniSystem.Core.Models;

public sealed class Course
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Code { get; init; }
    public string Name { get; set; }
    public int Credits { get; set; }
    public int Capacity { get; set; }
    public required Guid DepartmentId { get; init; }
    public Guid? LecturerId { get; set; }
    public CoursesStatus Status { get; set; } = CoursesStatus.Active;

    public override string ToString()
    {
        return $"{Code} - {Name} ({Credits} credits)";
    }
}
