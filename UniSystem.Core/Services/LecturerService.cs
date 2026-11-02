using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;

namespace UniSystem.Core.Services;

public sealed class LecturerService
{
    private readonly IRepository<Lecturer> _repository;
    private readonly DepartmentService _departmentService;

    public LecturerService(
        IRepository<Lecturer> repository,
        DepartmentService departmentService)
    {
        _repository = repository
            ?? throw new ArgumentNullException(nameof(repository));

        _departmentService = departmentService
            ?? throw new ArgumentNullException(nameof(departmentService));
    }

    public Lecturer RegisterLecturer(
        string staffNumber,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        string? title,
        string? office,
        string? departmentCode)
    {
        Guid? departmentId = null;

        if (!string.IsNullOrWhiteSpace(departmentCode))
        {
            Department department =
                _departmentService
                    .FindDepartmentByCode(departmentCode);

            departmentId = department.Id;
        }

        var lecturer = new Lecturer
        {
            StaffNumber = staffNumber,
            FirstName = firstName,
            LastName = lastName,
            Title = NormalizeOptional(title),
            Office = NormalizeOptional(office),
            DepartmentId = departmentId,
            ContactDetails = new ContactDetails(
                email,
                phoneNumber
            )
        };

        bool alreadyExists = _repository
            .GetAll()
            .Any(existingLecturer =>
                existingLecturer.StaffNumber.Equals(
                    lecturer.StaffNumber,
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (alreadyExists)
        {
            throw new DuplicateLecturerException(
                lecturer.StaffNumber
            );
        }

        _repository.Add(lecturer);

        return lecturer;
    }

    public IReadOnlyCollection<Lecturer> GetAllLecturers()
    {
        return _repository
            .GetAll()
            .OrderBy(lecturer => lecturer.StaffNumber)
            .ToList()
            .AsReadOnly();
    }

    public Lecturer FindLecturerByStaffNumber(
        string employeeNumber)
    {
        string normalizedNumber =
            employeeNumber?.Trim().ToUpperInvariant()
            ?? string.Empty;

        Lecturer? lecturer = _repository
            .GetAll()
            .FirstOrDefault(existingLecturer =>
                existingLecturer.StaffNumber.Equals(
                    normalizedNumber,
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (lecturer is null)
        {
            throw new LecturerNotFoundException(
                normalizedNumber
            );
        }

        return lecturer;
    }

    public IReadOnlyCollection<Lecturer> SearchLecturers(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new DomainValidationException(
                "Enter a lecturer name or employee number."
            );
        }

        string normalizedTerm = searchTerm.Trim();

        return _repository
            .GetAll()
            .Where(lecturer =>
                lecturer.StaffNumber.Contains(
                    normalizedTerm,
                    StringComparison.OrdinalIgnoreCase
                )
                || lecturer.FullName.Contains(
                    normalizedTerm,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            .OrderBy(lecturer => lecturer.StaffNumber)
            .ToList()
            .AsReadOnly();
    }

    public Lecturer UpdateLecturer(
        string employeeNumber,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        string? title,
        string? office)
    {
        Lecturer existingLecturer =
            FindLecturerByStaffNumber(employeeNumber);

        var updatedLecturer = new Lecturer
        {
            Id = existingLecturer.Id,
            StaffNumber =
                existingLecturer.StaffNumber,
            DepartmentId =
                existingLecturer.DepartmentId,
            FirstName = firstName,
            LastName = lastName,
            Title = NormalizeOptional(title),
            Office = NormalizeOptional(office),
            ContactDetails = new ContactDetails(
                email,
                phoneNumber
            )
        };

        _repository.Update(updatedLecturer);

        return updatedLecturer;
    }

    public Lecturer AssignLecturerToDepartment(
        string employeeNumber,
        string departmentCode)
    {
        Lecturer existingLecturer =
            FindLecturerByStaffNumber(employeeNumber);

        Department department =
            _departmentService
                .FindDepartmentByCode(departmentCode);

        var updatedLecturer = new Lecturer
        {
            Id = existingLecturer.Id,
            StaffNumber =
                existingLecturer.StaffNumber,
            FirstName = existingLecturer.FirstName,
            LastName = existingLecturer.LastName,
            Title = existingLecturer.Title,
            Office = existingLecturer.Office,
            ContactDetails =
                existingLecturer.ContactDetails,
            DepartmentId = department.Id
        };

        _repository.Update(updatedLecturer);

        return updatedLecturer;
    }

    public IReadOnlyCollection<Lecturer> GetLecturersByDepartment(string departmentCode)
    {
        Department department =
            _departmentService
                .FindDepartmentByCode(departmentCode);

        return _repository
            .GetAll()
            .Where(lecturer =>
                lecturer.DepartmentId == department.Id
            )
            .OrderBy(lecturer => lecturer.StaffNumber)
            .ToList()
            .AsReadOnly();
    }

    private static string? NormalizeOptional(
        string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}
