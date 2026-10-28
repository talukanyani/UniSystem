using UniSystem.Console.Menus;

public sealed class ConsoleMenu
{
    private readonly string _title;
    private readonly List<MenuOption> _options;
    private readonly string _exitLabel;

    public ConsoleMenu(
        string title,
        IEnumerable<MenuOption> options,
        string exitLabel = "Back")
    {
        _title = title;
        _options = options.ToList();
        _exitLabel = exitLabel;
    }

    public void Run()
    {
        while (true)
        {
            Display();

            Console.Write("Select an option: ");
            string? input = Console.ReadLine();

            if (input is null)
            {
                return;
            }

            string selectedKey = input.Trim();

            if (selectedKey == "0")
            {
                return;
            }

            MenuOption? selectedOption =
                _options.FirstOrDefault(
                    option => option.Key.Equals(
                        selectedKey,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            if (selectedOption is null)
            {
                ShowInvalidOption();
                continue;
            }

            selectedOption.Execute();
        }
    }

    private void Display()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================");
        Console.WriteLine($" {_title}");
        Console.WriteLine("==============================");
        Console.ResetColor();

        Console.WriteLine();

        foreach (MenuOption option in _options)
        {
            Console.WriteLine(
                $"{option.Key}. {option.Label}"
            );
        }

        Console.WriteLine($"0. {_exitLabel}");
        Console.WriteLine();
    }

    private static void ShowInvalidOption()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine(
            "Invalid option. Please try again."
        );
        Console.ResetColor();

        Pause();
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey(intercept: true);
    }
}
