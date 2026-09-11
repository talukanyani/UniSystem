using UniSystem.Core.Exceptions;
using UniSystem.Core.Interfaces;
using UniSystem.Core.Models;
using UniSystem.Infrastructure.Repositories;

namespace UniSystem.Tests.Repositories;

public sealed class InMemoryRepositoryTests
{
    [Fact]
    public void Add_ValidStudent_StoresStudent()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        Student student = CreateStudent();

        repository.Add(student);

        Student? savedStudent =
            repository.GetById(student.Id);

        Assert.Same(student, savedStudent);
        Assert.True(repository.Exists(student.Id));
    }

    [Fact]
    public void Add_DuplicateId_ThrowsException()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        Student student = CreateStudent();

        repository.Add(student);

        Assert.Throws<DuplicateEntityException>(
            () => repository.Add(student)
        );
    }

    [Fact]
    public void GetAll_AfterAddingStudents_ReturnsAllStudents()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        repository.Add(CreateStudent("24044219"));
        repository.Add(CreateStudent("24044220"));

        IReadOnlyCollection<Student> students =
            repository.GetAll();

        Assert.Equal(2, students.Count);
    }

    [Fact]
    public void Remove_ExistingStudent_RemovesStudent()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        Student student = CreateStudent();

        repository.Add(student);

        bool wasRemoved = repository.Remove(student.Id);

        Assert.True(wasRemoved);
        Assert.False(repository.Exists(student.Id));
        Assert.Null(repository.GetById(student.Id));
    }

    [Fact]
    public void Remove_MissingStudent_ReturnsFalse()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        bool wasRemoved = repository.Remove(Guid.NewGuid());

        Assert.False(wasRemoved);
    }

    [Fact]
    public void Update_ExistingStudent_ReplacesStudent()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        Student originalStudent = CreateStudent();

        repository.Add(originalStudent);

        var updatedStudent = new Student
        {
            Id = originalStudent.Id,
            StudentNumber = originalStudent.StudentNumber,
            FirstName = "Updated",
            LastName = "Student"
        };

        repository.Update(updatedStudent);

        Student? savedStudent =
            repository.GetById(originalStudent.Id);

        Assert.Same(updatedStudent, savedStudent);
        Assert.Equal("Updated", savedStudent?.FirstName);
    }

    [Fact]
    public void Update_MissingStudent_ThrowsException()
    {
        IRepository<Student> repository =
            new InMemoryRepository<Student>();

        Student student = CreateStudent();

        Assert.Throws<EntityNotFoundException>(
            () => repository.Update(student)
        );
    }

    private static Student CreateStudent(
        string studentNumber = "24044229")
    {
        return new Student
        {
            StudentNumber = studentNumber,
            FirstName = "Thendo",
            LastName = "Mudau"
        };
    }
}
