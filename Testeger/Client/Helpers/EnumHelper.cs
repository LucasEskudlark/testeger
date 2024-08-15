using System.Text.RegularExpressions;

namespace Testeger.Client.Helpers;

public static class EnumHelper
{
    public static string[] GetSpacedNames(Type enumType)
    {
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Type must be an enumeration", nameof(enumType));
        }

        return Enum.GetNames(enumType)
                   .Select(name => Regex.Replace(name, "(?<!^)([A-Z])", " $1"))
                   .ToArray();
    }
    public static string AddSpacesToEnumName(string enumName)
    {
        return string.Concat(enumName.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart();
    }
}
