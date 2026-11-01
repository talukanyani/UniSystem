using UniSystem.Console.Menus;
using UniSystem.Core.Exceptions;
using UniSystem.Core.Models;
using UniSystem.Core.Services;

public sealed class StudentMenu
{
    private readonly StudentService _studentService;
    private readonly ConsoleMenu _menu;

    public StudentMenu(StudentService studentService)
    {
        _studentService = studentService;

        _menu = new ConsoleMenu(
            "Student Management",
            new List<MenuOption>
            {
                new(
                    "1",
                    "Register a student",
                    () => ExecuteSafely(RegisterStudent)
                ),
                new(
                    "2",
                    "List all students",
                    () => ExecuteSafely(ListStudents)
                ),
                new(
                    "3",
                    "Search for a student",
                    () => ExecuteSafely(SearchStudents)
                ),
                new(
                    "4",
                    "Update a student",
                    () => ExecuteSafely(UpdateStudent)
                ),
                new(
                    "5",
                    "Remove a student",
                    () => ExecuteSafely(RemoveStudent)
                )
            },
            "Return to main menu"
        );
    }

    public void Run()
    {
        _menu.Run();
    }

    private void RegisterStudent()
    {
        ShowHeading("Register Student");

        string studentNumber =
            ReadInput("Student number: ");

        string firstName =
            ReadInput("First name: ");

        string lastName =
            ReadInput("Last name: ");

        string email =
            ReadInput("Email address: ");

        string phoneNumber =
            ReadInput("Phone number (optional): ");

        Student student =
            _studentService.RegisterStudent(
                studentNumber,
                firstName,
                lastName,
                email,
                phoneNumber
            );

        ShowSuccess(
            $"Student {student.StudentNumber} " +
            $"was registered successfully."
        );

        Pause();
    }

    private void ListStudents()
    {
        ShowHeading("Registered Students");

        IReadOnlyCollection<Student> students =
            _studentService.GetAllStudents();

        DisplayStudents(students);

        Pause();
    }

    private void SearchStudents()
    {
        ShowHeading("Search Students");

        string searchTerm =
            ReadInput("Name or student number: ");

        IReadOnlyCollection<Student> students =
            _studentService.SearchStudents(searchTerm);

        DisplayStudents(students);

        Pause();
    }

    private void UpdateStudent()
    {
        ShowHeading("Update Student");

        string studentNumber =
            ReadInput("Student number: ");

        Student existingStudent =
            _studentService.FindStudentByNumber(
                studentNumber
            );

        Console.WriteLine();
        Console.WriteLine(
            "Press Enter to keep the current value."
        );
        Console.WriteLine();

        string firstName = ReadUpdatedValue(
            "First name",
            existingStudent.FirstName
        );

        string lastName = ReadUpdatedValue(
            "Last name",
            existingStudent.LastName
        );

        string currentEmail =
            existingStudent.ContactDetails?.Email
            ?? string.Empty;

        string email = ReadUpdatedValue(
            "Email",
            currentEmail
        );

        string currentPhone =
            existingStudent.ContactDetails?.PhoneNumber
            ?? string.Empty;

        string phoneNumber = ReadUpdatedValue(
            "Phone number",
            currentPhone
        );

        Student updatedStudent =
            _studentService.UpdateStudent(
                studentNumber,
                firstName,
                lastName,
                email,
                phoneNumber
            );

        ShowSuccess(
            $"{updatedStudent.StudentNumber} " +
            "was updated successfully."
        );

        Pause();
    }

    private void RemoveStudent()
    {
        ShowHeading("Remove Student");

        string studentNumber =
            ReadInput("Student number: ");

        Student student =
            _studentService.FindStudentByNumber(
                studentNumber
            );

        Console.WriteLine();
        Console.WriteLine($"Student: {student.FullName}");

        string confirmation = ReadInput(
            "Remove this student? (y/n): "
        );

        if (!confirmation.Equals(
                "y",
                StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine();
            Console.WriteLine("Removal cancelled.");
            Pause();
            return;
        }

        _studentService.RemoveStudent(studentNumber);

        ShowSuccess(
            $"{student.StudentNumber} was removed."
        );

        Pause();
    }

    private static void DisplayStudents(
        IEnumerable<Student> students)
    {
        List<Student> studentList =
            students.ToList();

        Console.WriteLine();

        if (studentList.Count == 0)
        {
            Console.WriteLine("No students were found.");
            return;
        }

        Console.WriteLine(
            $"{"Student No.",-15}" +
            $"{"Name",-25}" +
            $"{"Email",-35}" +
            $"{"Status",-12}"
        );

        Console.WriteLine(new string('-', 87));

        foreach (Student student in studentList)
        {
            string email =
                student.ContactDetails?.Email ?? "-";

            Console.WriteLine(
                $"{student.StudentNumber,-15}" +
                $"{student.FullName,-25}" +
                $"{email,-35}" +
                $"{student.Status,-12}"
            );
        }
    }

    private static string ReadInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private static string ReadUpdatedValue(
        string label,
        string currentValue)
    {
        Console.Write($"{label} [{currentValue}]: ");

        string? newValue = Console.ReadLine();

        return string.IsNullOrWhiteSpace(newValue)
            ? currentValue
            : newValue.Trim();
    }

    private static void ExecuteSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception) when (
            exception is DomainValidationException
            or DuplicateStudentException
            or StudentNotFoundException)
        {
            ShowError(exception.Message);
            Pause();
        }
    }

    private static void ShowHeading(string heading)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================");
        Console.WriteLine($" {heading}");
        Console.WriteLine("==============================");
        Console.ResetColor();

        Console.WriteLine();
    }

    private static void ShowSuccess(string message)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void ShowError(string message)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey(intercept: true);
    }
}
