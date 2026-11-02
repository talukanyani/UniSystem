using UniSystem.Console.Menus;

public sealed class MainMenu
{
    private readonly StudentMenu _studentMenu;
    private readonly LecturerMenu _lecturerMenu;

    public MainMenu(
        StudentMenu studentMenu,
        LecturerMenu lecturerMenu)
    {
        _studentMenu = studentMenu;
        _lecturerMenu = lecturerMenu;
    }

    public void Run()
    {
        var mainMenu = new ConsoleMenu(
            "UniSystem Main Menu",
            new List<MenuOption>
            {
                new(
                    "1",
                    "Manage students",
                    _studentMenu.Run
                    ),
                new(
                    "2",
                    "Manage lecturers",
                    _lecturerMenu.Run
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
