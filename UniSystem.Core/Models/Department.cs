namespace UniSystem.Core.Models;

public sealed class Department
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Code { get; init; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Code} - {Name}";
    }
}
