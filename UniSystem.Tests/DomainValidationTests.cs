using UniSystem.Core.Exceptions;
using UniSystem.Core.Models;
using Xunit;

namespace UniSystem.Tests;

public sealed class DomainValidationTests
{
    [Fact]
    public void Student_WithInvalidStudentNumber_ThrowsException()
    {
        Action createStudent = () => new Student
        {
            StudentNumber = "ABC123",
            FirstName = "Thendo",
            LastName = "Mudau"
        };

        Assert.Throws<InvalidStudentNumberException>(
            createStudent
        );
    }

    [Theory]
    [InlineData("CS101")]
    [InlineData("CSC-101")]
    [InlineData("123456")]
    public void Course_WithInvalidCode_ThrowsException(
        string invalidCode)
    {
        Action createCourse = () => new Course
        {
            Code = invalidCode,
            Name = "Programming",
            Credits = 16,
            Capacity = 50,
            DepartmentId = Guid.NewGuid()
        };

        Assert.Throws<InvalidCourseCodeException>(
            createCourse
        );
    }

    [Fact]
    public void ContactDetails_WithInvalidEmail_ThrowsException()
    {
        Action createContactDetails = () =>
            new ContactDetails("not-an-email", null);

        Assert.Throws<InvalidEmailAddressException>(
            createContactDetails
        );
    }

    [Fact]
    public void Course_WithValidValues_IsCreated()
    {
        var course = new Course
        {
            Code = "csc101",
            Name = "Introduction to Programming",
            Credits = 16,
            Capacity = 50,
            DepartmentId = Guid.NewGuid()
        };

        Assert.Equal("CSC101", course.Code);
        Assert.Equal(16, course.Credits);
        Assert.Equal(50, course.Capacity);
    }
}
