using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Infrastructure.Repositories;
using Xunit;

namespace UniSystem.Tests.Services;

public sealed class LecturerServiceTests
{
    [Fact]
    public void CreateDepartment_DuplicateCode_ThrowsException()
    {
        var (departmentService, _) = CreateServices();

        departmentService.CreateDepartment(
            "CS",
            "Computer Science"
        );

        Assert.Throws<DuplicateDepartmentException>(
            () => departmentService.CreateDepartment(
                "cs",
                "Another Department"
            )
        );
    }

    [Fact]
    public void RegisterLecturer_DuplicateNumber_ThrowsException()
    {
        var (_, lecturerService) = CreateServices();

        RegisterLecturer(lecturerService);

        Assert.Throws<DuplicateLecturerException>(
            () => RegisterLecturer(lecturerService)
        );
    }

    [Fact]
    public void RegisterLecturer_UnknownDepartment_ThrowsException()
    {
        var (_, lecturerService) = CreateServices();

        Assert.Throws<DepartmentNotFoundException>(
            () => lecturerService.RegisterLecturer(
                "EMP001",
                "Rudzani",
                "Mulaudzi",
                "rudzani@example.com",
                null,
                "Dr",
                "B12",
                "UNKNOWN"
            )
        );
    }

    [Fact]
    public void AssignLecturerToDepartment_AssignsDepartment()
    {
        var (departmentService, lecturerService) =
            CreateServices();

        Department department =
            departmentService.CreateDepartment(
                "CS",
                "Computer Science"
            );

        RegisterLecturer(lecturerService);

        Lecturer lecturer =
            lecturerService.AssignLecturerToDepartment(
                "EMP001",
                "CS"
            );

        Assert.Equal(
            department.Id,
            lecturer.DepartmentId
        );
    }

    [Fact]
    public void GetLecturersByDepartment_ReturnsAssignedLecturers()
    {
        var (departmentService, lecturerService) =
            CreateServices();

        departmentService.CreateDepartment(
            "CS",
            "Computer Science"
        );

        RegisterLecturer(lecturerService);

        lecturerService.AssignLecturerToDepartment(
            "EMP001",
            "CS"
        );

        IReadOnlyCollection<Lecturer> lecturers =
            lecturerService.GetLecturersByDepartment(
                "CS"
            );

        Assert.Single(lecturers);
    }

    private static Lecturer RegisterLecturer(
        LecturerService service)
    {
        return service.RegisterLecturer(
            "EMP001",
            "Rudzani",
            "Mulaudzi",
            "rudzani@example.com",
            null,
            "Dr",
            "B12",
            null
        );
    }

    private static (
        DepartmentService DepartmentService,
        LecturerService LecturerService
    ) CreateServices()
    {
        IRepository<Department> departmentRepository =
            new InMemoryRepository<Department>();

        IRepository<Lecturer> lecturerRepository =
            new InMemoryRepository<Lecturer>();

        var departmentService =
            new DepartmentService(
                departmentRepository
            );

        var lecturerService =
            new LecturerService(
                lecturerRepository,
                departmentService
            );

        return (
            departmentService,
            lecturerService
        );
    }
}
