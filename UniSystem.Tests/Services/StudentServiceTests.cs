using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Infrastructure.Repositories;
using Xunit;

namespace UniSystem.Tests.Services;

public sealed class StudentServiceTests
{
    [Fact]
    public void RegisterStudent_ValidData_AddsStudent()
    {
        StudentService service = CreateService();

        Student student = service.RegisterStudent(
            "24044238",
            "Thendo",
            "Mudau",
            "thendo@example.com",
            null
        );

        Assert.Equal("24044238", student.StudentNumber);
        Assert.Single(service.GetAllStudents());
    }

    [Fact]
    public void RegisterStudent_DuplicateNumber_ThrowsException()
    {
        StudentService service = CreateService();

        service.RegisterStudent(
            "24044238",
            "Thendo",
            "Mudau",
            "thendo@example.com",
            null
        );

        Assert.Throws<DuplicateStudentException>(
            () => service.RegisterStudent(
                "24044238",
                "Another",
                "Student",
                "another@example.com",
                null
            )
        );
    }

    [Fact]
    public void SearchStudents_NameIsCaseInsensitive()
    {
        StudentService service = CreateService();

        service.RegisterStudent(
            "24044238",
            "Thendo",
            "Mudau",
            "thendo@example.com",
            null
        );

        IReadOnlyCollection<Student> results =
            service.SearchStudents("thendo");

        Assert.Single(results);
    }

    [Fact]
    public void UpdateStudent_PreservesIdentity()
    {
        StudentService service = CreateService();

        Student originalStudent =
            service.RegisterStudent(
                "24044238",
                "Thendo",
                "Mudau",
                "thendo@example.com",
                null
            );

        Student updatedStudent =
            service.UpdateStudent(
                "24044238",
                "Updated",
                "Name",
                "updated@example.com",
                null
            );

        Assert.Equal(
            originalStudent.Id,
            updatedStudent.Id
        );

        Assert.Equal(
            originalStudent.StudentNumber,
            updatedStudent.StudentNumber
        );

        Assert.Equal("Updated", updatedStudent.FirstName);
    }

    [Fact]
    public void RemoveStudent_ExistingStudent_RemovesStudent()
    {
        StudentService service = CreateService();

        service.RegisterStudent(
            "24044238",
            "Thendo",
            "Mudau",
            "thendo@example.com",
            null
        );

        service.RemoveStudent("24044238");

        Assert.Empty(service.GetAllStudents());
    }

    private static StudentService CreateService()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        return new StudentService(repository);
    }
}
