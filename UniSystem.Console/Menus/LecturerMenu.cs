using UniSystem.Console.Menus;
using UniSystem.Core.Exceptions;
using UniSystem.Core.Models;
using UniSystem.Core.Services;

public sealed class LecturerMenu
{
    private readonly LecturerService _lecturerService;
    private readonly DepartmentService _departmentService;
    private readonly ConsoleMenu _menu;

    public LecturerMenu(
        LecturerService lecturerService,
        DepartmentService departmentService)
    {
        _lecturerService = lecturerService;
        _departmentService = departmentService;

        _menu = new ConsoleMenu(
            "Lecturer and Department Management",
            new List<MenuOption>
            {
                new(
                    "1",
                    "Create a department",
                    () => ExecuteSafely(CreateDepartment)
                ),
                new(
                    "2",
                    "List departments",
                    () => ExecuteSafely(ListDepartments)
                ),
                new(
                    "3",
                    "Register a lecturer",
                    () => ExecuteSafely(RegisterLecturer)
                ),
                new(
                    "4",
                    "List lecturers",
                    () => ExecuteSafely(ListLecturers)
                ),
                new(
                    "5",
                    "Search lecturers",
                    () => ExecuteSafely(SearchLecturers)
                ),
                new(
                    "6",
                    "Update a lecturer",
                    () => ExecuteSafely(UpdateLecturer)
                ),
                new(
                    "7",
                    "Assign lecturer to department",
                    () => ExecuteSafely(AssignDepartment)
                ),
                new(
                    "8",
                    "List lecturers in a department",
                    () => ExecuteSafely(
                        ListDepartmentLecturers
                    )
                )
            },
            "Return to main menu"
        );
    }

    public void Run()
    {
        _menu.Run();
    }

    private void CreateDepartment()
    {
        ShowHeading("Create Department");

        string code = ReadInput("Department code: ");
        string name = ReadInput("Department name: ");

        Department department =
            _departmentService.CreateDepartment(
                code,
                name
            );

        ShowSuccess(
            $"Department {department.Code} was created."
        );

        Pause();
    }

    private void ListDepartments()
    {
        ShowHeading("Departments");

        IReadOnlyCollection<Department> departments =
            _departmentService.GetAllDepartments();

        if (departments.Count == 0)
        {
            Console.WriteLine(
                "No departments have been created."
            );
        }
        else
        {
            Console.WriteLine(
                $"{"Code",-12}{"Department name",-40}"
            );

            Console.WriteLine(new string('-', 52));

            foreach (Department department in departments)
            {
                Console.WriteLine(
                    $"{department.Code,-12}" +
                    $"{department.Name,-40}"
                );
            }
        }

        Pause();
    }

    private void RegisterLecturer()
    {
        ShowHeading("Register Lecturer");

        string employeeNumber =
            ReadInput("Employee number: ");

        string firstName =
            ReadInput("First name: ");

        string lastName =
            ReadInput("Last name: ");

        string email =
            ReadInput("Email: ");

        string phone =
            ReadInput("Phone number (optional): ");

        string title =
            ReadInput("Title, for example Dr (optional): ");

        string office =
            ReadInput("Office (optional): ");

        string departmentCode =
            ReadInput(
                "Department code (optional): "
            );

        Lecturer lecturer =
            _lecturerService.RegisterLecturer(
                employeeNumber,
                firstName,
                lastName,
                email,
                phone,
                title,
                office,
                departmentCode
            );

        ShowSuccess(
            $"Lecturer {lecturer.StaffNumber} " +
            "was registered."
        );

        Pause();
    }

    private void ListLecturers()
    {
        ShowHeading("Lecturers");

        DisplayLecturers(
            _lecturerService.GetAllLecturers()
        );

        Pause();
    }

    private void SearchLecturers()
    {
        ShowHeading("Search Lecturers");

        string searchTerm =
            ReadInput("Name or employee number: ");

        DisplayLecturers(
            _lecturerService.SearchLecturers(searchTerm)
        );

        Pause();
    }

    private void UpdateLecturer()
    {
        ShowHeading("Update Lecturer");

        string employeeNumber =
            ReadInput("Employee number: ");

        Lecturer existing =
            _lecturerService
                .FindLecturerByStaffNumber(
                    employeeNumber
                );

        Console.WriteLine();
        Console.WriteLine(
            "Press Enter to retain the current value."
        );
        Console.WriteLine();

        string firstName = ReadUpdatedValue(
            "First name",
            existing.FirstName
        );

        string lastName = ReadUpdatedValue(
            "Last name",
            existing.LastName
        );

        string email = ReadUpdatedValue(
            "Email",
            existing.ContactDetails?.Email
                ?? string.Empty
        );

        string phone = ReadUpdatedValue(
            "Phone",
            existing.ContactDetails?.PhoneNumber
                ?? string.Empty
        );

        string title = ReadUpdatedValue(
            "Title",
            existing.Title ?? string.Empty
        );

        string office = ReadUpdatedValue(
            "Office",
            existing.Office ?? string.Empty
        );

        Lecturer updated =
            _lecturerService.UpdateLecturer(
                employeeNumber,
                firstName,
                lastName,
                email,
                phone,
                title,
                office
            );

        ShowSuccess(
            $"{updated.StaffNumber} was updated."
        );

        Pause();
    }

    private void AssignDepartment()
    {
        ShowHeading("Assign Department");

        string employeeNumber =
            ReadInput("Employee number: ");

        string departmentCode =
            ReadInput("Department code: ");

        Lecturer lecturer =
            _lecturerService
                .AssignLecturerToDepartment(
                    employeeNumber,
                    departmentCode
                );

        Department department =
            _departmentService.FindDepartmentById(
                lecturer.DepartmentId!.Value
            );

        ShowSuccess(
            $"{lecturer.StaffNumber} was assigned " +
            $"to {department.Code}."
        );

        Pause();
    }

    private void ListDepartmentLecturers()
    {
        ShowHeading("Department Lecturers");

        string departmentCode =
            ReadInput("Department code: ");

        Department department =
            _departmentService
                .FindDepartmentByCode(departmentCode);

        Console.WriteLine();
        Console.WriteLine(
            $"{department.Code} - {department.Name}"
        );

        DisplayLecturers(
            _lecturerService
                .GetLecturersByDepartment(
                    department.Code
                )
        );

        Pause();
    }

    private void DisplayLecturers(IEnumerable<Lecturer> lecturers)
    {
        List<Lecturer> lecturerList = lecturers.ToList();

        Console.WriteLine();

        if (lecturerList.Count == 0)
        {
            Console.WriteLine("No lecturers were found.");
            return;
        }

        Console.WriteLine(
            $"{"Employee",-12}" +
            $"{"Name",-26}" +
            $"{"Title",-12}" +
            $"{"Department",-12}"
        );

        Console.WriteLine(new string('-', 62));

        foreach (Lecturer lecturer in lecturerList)
        {
            string departmentCode = "Unassigned";

            if (lecturer.DepartmentId.HasValue)
            {
                departmentCode = _departmentService
                    .FindDepartmentById(
                        lecturer.DepartmentId.Value
                    )
                    .Code;
            }

            Console.WriteLine(
                $"{lecturer.StaffNumber,-12}" +
                $"{lecturer.FullName,-26}" +
                $"{lecturer.Title ?? "-",-12}" +
                $"{departmentCode,-12}"
            );
        }
    }

    private static void ExecuteSafely(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception) when (
            exception is DomainValidationException
            or DuplicateDepartmentException
            or DepartmentNotFoundException
            or DuplicateLecturerException
            or LecturerNotFoundException)
        {
            ShowError(exception.Message);
            Pause();
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

        string? value = Console.ReadLine();

        return string.IsNullOrWhiteSpace(value)
            ? currentValue
            : value.Trim();
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
