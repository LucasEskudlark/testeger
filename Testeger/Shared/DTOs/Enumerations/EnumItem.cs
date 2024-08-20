namespace Testeger.Shared.DTOs.Enumerations;

public class EnumItem<T>
{
    public required T Value { get; set; }
    public required string Name { get; set; }
}
