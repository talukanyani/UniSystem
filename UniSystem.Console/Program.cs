using UniSystem.Core.Models;

Console.Title = "UniSystem";

Console.WriteLine("========================");
Console.WriteLine(" Welcome to UniSystem");
Console.WriteLine("========================");
Console.WriteLine();

var department = new Department
{
    Code = "MATCS",
    Name = "Department of Mathematical and Computational Sciences"
};

var lecturer = new Lecturer
{
    StaffNumber = "STN0001",
    FirstName = "John",
    LastName = "Mudau",
    Title = "Dr.",
    Office = "Room 101",
    DepartmentId = department.Id,
    ContactDetails = new ContactDetails(
        "jmudau@unisystem.ac.za",
        null
        )
};

var student = new Student
{
    StudentNumber = "24044228",
    FirstName = "Talu",
    LastName = "Mutshaeni",
    ContactDetails = new ContactDetails(
        "24044228@student.unisystem.ac.za",
        "0831234567"
        )
};

var course = new Course
{
    Code = "COM201",
    Name = "Software Engineering",
    Credits = 16,
    Capacity = 70,
    DepartmentId = department.Id,
    LecturerId = lecturer.Id
};

Console.WriteLine("Department:");
Console.WriteLine(department);

Console.WriteLine("\nLecturer:");
Console.WriteLine(lecturer);

Console.WriteLine("\nStudent:");
Console.WriteLine(student);

Console.WriteLine("\nCourse:");
Console.WriteLine(course);
