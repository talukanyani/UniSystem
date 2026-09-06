using System.Text.RegularExpressions;
using UniSystem.Core.Enums;
using UniSystem.Core.Exceptions;

namespace UniSystem.Core.Models;

public sealed class Student : Person
{
    private static readonly Regex StudentNumberPattern =
        new(@"^\d{8}$", RegexOptions.Compiled);

    private string _studentNumber = string.Empty;

    public string StudentNumber
    {
        get => _studentNumber;

        init
        {
            string normalizedNumber = value?.Trim() ?? string.Empty;

            if (!StudentNumberPattern.IsMatch(normalizedNumber))
            {
                throw new InvalidStudentNumberException(value);
            }

            _studentNumber = normalizedNumber;
        }
    }

    public DateOnly RegistrationDate { get; init; }
        = DateOnly.FromDateTime(DateTime.Today);

    public StudentStatus Status { get; set; } = StudentStatus.Active;

    public override string ToString()
    {
        return $"{StudentNumber} - {base.ToString()} [{Status}]";
    }
}
