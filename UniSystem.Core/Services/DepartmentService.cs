using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;

namespace UniSystem.Core.Services;

public sealed class DepartmentService
{
    private readonly IRepository<Department> _repository;

    public DepartmentService(IRepository<Department> repository)
    {
        _repository = repository
            ?? throw new ArgumentNullException(nameof(repository));
    }

    public Department CreateDepartment(string code, string name)
    {
        var department = new Department
        {
            Code = code,
            Name = name
        };

        bool alreadyExists = _repository
            .GetAll()
            .Any(existingDepartment =>
                existingDepartment.Code.Equals(
                    department.Code,
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (alreadyExists)
        {
            throw new DuplicateDepartmentException(
                department.Code
            );
        }

        _repository.Add(department);

        return department;
    }

    public IReadOnlyCollection<Department> GetAllDepartments()
    {
        return _repository
            .GetAll()
            .OrderBy(department => department.Code)
            .ToList()
            .AsReadOnly();
    }

    public Department FindDepartmentByCode(string code)
    {
        string normalizedCode =
            code?.Trim().ToUpperInvariant()
            ?? string.Empty;

        Department? department = _repository
            .GetAll()
            .FirstOrDefault(existingDepartment =>
                existingDepartment.Code.Equals(
                    normalizedCode,
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (department is null)
        {
            throw new DepartmentNotFoundException(
                normalizedCode
            );
        }

        return department;
    }

    public Department FindDepartmentById(Guid id)
    {
        Department? department =
            _repository.GetById(id);

        if (department is null)
        {
            throw new DepartmentNotFoundException(id);
        }

        return department;
    }
}
