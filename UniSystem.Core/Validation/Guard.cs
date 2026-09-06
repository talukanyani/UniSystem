using UniSystem.Core.Exceptions;

namespace UniSystem.Core.Validation;

internal static class Guard
{
    public static string RequiredText(
        string? value,
        string fieldName,
        int maximumLength = 100)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainValidationException(
                $"{fieldName} is required."
            );
        }

        string trimmedValue = value.Trim();

        if (trimmedValue.Length > maximumLength)
        {
            throw new DomainValidationException(
                $"{fieldName} cannot exceed {maximumLength} characters."
            );
        }

        return trimmedValue;
    }

    public static int InRange(
        int value,
        int minimum,
        int maximum,
        string fieldName)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainValidationException(
                $"{fieldName} must be between {minimum} and {maximum}."
            );
        }

        return value;
    }

    public static Guid NotEmpty(Guid value, string fieldName)
    {
        if (value == Guid.Empty)
        {
            throw new DomainValidationException(
                $"{fieldName} cannot be empty."
            );
        }

        return value;
    }
}
