using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;

namespace UniSystem.Core.Services;

public sealed class StudentService
{
    private readonly IRepository<Student> _repository;

    public StudentService(IRepository<Student> repository)
    {
        _repository = repository
            ?? throw new ArgumentNullException(
                nameof(repository)
            );
    }

    public Student RegisterStudent(
        string studentNumber,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber)
    {
        var student = new Student
        {
            StudentNumber = studentNumber,
            FirstName = firstName,
            LastName = lastName,
            ContactDetails = new ContactDetails(
                email,
                phoneNumber
            )
        };

        bool alreadyExists = _repository
            .GetAll()
            .Any(existingStudent =>
                existingStudent.StudentNumber
                    == student.StudentNumber
            );

        if (alreadyExists)
        {
            throw new DuplicateStudentException(
                student.StudentNumber
            );
        }

        _repository.Add(student);

        return student;
    }

    public IReadOnlyCollection<Student> GetAllStudents()
    {
        return _repository
            .GetAll()
            .OrderBy(student => student.StudentNumber)
            .ToList()
            .AsReadOnly();
    }

    public Student FindStudentByNumber(
        string studentNumber)
    {
        string normalizedNumber =
            studentNumber?.Trim() ?? string.Empty;

        Student? student = _repository
            .GetAll()
            .FirstOrDefault(existingStudent =>
                existingStudent.StudentNumber
                    == normalizedNumber
            );

        if (student is null)
        {
            throw new StudentNotFoundException(
                normalizedNumber
            );
        }

        return student;
    }

    public IReadOnlyCollection<Student> SearchStudents(
        string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new DomainValidationException(
                "Enter a name or student number to search."
            );
        }

        string normalizedTerm = searchTerm.Trim();

        return _repository
            .GetAll()
            .Where(student =>
                student.StudentNumber.Contains(
                    normalizedTerm,
                    StringComparison.OrdinalIgnoreCase
                )
                || student.FullName.Contains(
                    normalizedTerm,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            .OrderBy(student => student.StudentNumber)
            .ToList()
            .AsReadOnly();
    }

    public Student UpdateStudent(
        string studentNumber,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber)
    {
        Student existingStudent =
            FindStudentByNumber(studentNumber);

        var updatedStudent = new Student
        {
            Id = existingStudent.Id,
            StudentNumber = existingStudent.StudentNumber,
            RegistrationDate =
                existingStudent.RegistrationDate,
            Status = existingStudent.Status,
            FirstName = firstName,
            LastName = lastName,
            ContactDetails = new ContactDetails(
                email,
                phoneNumber
            )
        };

        _repository.Update(updatedStudent);

        return updatedStudent;
    }

    public void RemoveStudent(string studentNumber)
    {
        Student student =
            FindStudentByNumber(studentNumber);

        bool wasRemoved =
            _repository.Remove(student.Id);

        if (!wasRemoved)
        {
            throw new StudentNotFoundException(
                studentNumber
            );
        }
    }
}
