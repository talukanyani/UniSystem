using UniSystem.Console.Menus;

public sealed class MainMenu
{
    public void Run()
    {
        ConsoleMenu studentMenu = CreateStudentMenu();

        var mainMenu = new ConsoleMenu(
            "UniSystem Main Menu",
            new List<MenuOption>
            {
                new(
                    "1",
                    "Manage students",
                    studentMenu.Run
                ),
                new(
                    "2",
                    "Manage lecturers",
                    () => ShowComingSoon(
                        "Lecturer management"
                    )
                ),
                new(
                    "3",
                    "Manage courses",
                    () => ShowComingSoon(
                        "Course management"
                    )
                ),
                new(
                    "4",
                    "Manage enrolments",
                    () => ShowComingSoon(
                        "Enrolment management"
                    )
                ),
                new(
                    "5",
                    "Generate reports",
                    () => ShowComingSoon(
                        "Report generation"
                    )
                )
            },
            "Exit UniSystem"
        );

        mainMenu.Run();
    }

    private static ConsoleMenu CreateStudentMenu()
    {
        return new ConsoleMenu(
            "Student Management",
            new List<MenuOption>
            {
                new(
                    "1",
                    "Register a student",
                    () => ShowComingSoon(
                        "Student registration"
                    )
                ),
                new(
                    "2",
                    "List all students",
                    () => ShowComingSoon(
                        "Student listing"
                    )
                ),
                new(
                    "3",
                    "Search for a student",
                    () => ShowComingSoon(
                        "Student searching"
                    )
                ),
                new(
                    "4",
                    "Update a student",
                    () => ShowComingSoon(
                        "Student updating"
                    )
                ),
                new(
                    "5",
                    "Remove a student",
                    () => ShowComingSoon(
                        "Student removal"
                    )
                )
            },
            "Return to main menu"
        );
    }

    private static void ShowComingSoon(string feature)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(feature);
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine(
            "This feature will be implemented in a future task."
        );

        Console.WriteLine();
        Console.Write("Press any key to return...");
        Console.ReadKey(intercept: true);
    }
}
