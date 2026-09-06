using System.Text.RegularExpressions;
using UniSystem.Core.Enums;
using UniSystem.Core.Exceptions;
using UniSystem.Core.Validation;

namespace UniSystem.Core.Models;

public sealed class Course
{
    private static readonly Regex CourseCodePattern =
        new(@"^[A-Z]{3}\d{3}$", RegexOptions.Compiled);

    private string _code = string.Empty;
    private string _name = string.Empty;
    private int _credits;
    private int _capacity;
    private Guid _departmentId;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Code
    {
        get => _code;

        init
        {
            string normalizedCode =
                value?.Trim().ToUpperInvariant() ?? string.Empty;

            if (!CourseCodePattern.IsMatch(normalizedCode))
            {
                throw new InvalidCourseCodeException(value!);
            }

            _code = normalizedCode;
        }
    }

    public string Name
    {
        get => _name;

        set => _name = Guard.RequiredText(
            value,
            nameof(Name),
            100
        );
    }

    public int Credits
    {
        get => _credits;

        set => _credits = Guard.InRange(
            value,
            1,
            60,
            nameof(Credits)
        );
    }

    public int Capacity
    {
        get => _capacity;

        set => _capacity = Guard.InRange(
            value,
            1,
            500,
            nameof(Capacity)
        );
    }

    public Guid DepartmentId
    {
        get => _departmentId;

        init => _departmentId = Guard.NotEmpty(
            value,
            nameof(DepartmentId)
        );
    }

    public Guid? LecturerId { get; set; }

    public CourseStatus Status { get; set; }
        = CourseStatus.Active;

    public override string ToString()
    {
        return $"{Code} - {Name} ({Credits} credits)";
    }
}
