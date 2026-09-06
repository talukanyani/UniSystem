using System.Text.RegularExpressions;
using UniSystem.Core.Exceptions;
using UniSystem.Core.Validation;

namespace UniSystem.Core.Models;

public sealed class Department
{
    private static readonly Regex CodePattern =
        new(@"^[A-Z]{2,6}$", RegexOptions.Compiled);

    private string _code = string.Empty;
    private string _name = string.Empty;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Code
    {
        get => _code;

        init
        {
            string normalizedCode =
                value?.Trim().ToUpperInvariant() ?? string.Empty;

            if (!CodePattern.IsMatch(normalizedCode))
            {
                throw new DomainValidationException(
                    "A department code must contain between " +
                    "2 and 6 letters."
                );
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

    public override string ToString()
    {
        return $"{Code} - {Name}";
    }
}
