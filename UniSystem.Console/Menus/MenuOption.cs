namespace UniSystem.Console.Menus;

public sealed record MenuOption(
    string Key,
    string Label,
    Action Execute
);
