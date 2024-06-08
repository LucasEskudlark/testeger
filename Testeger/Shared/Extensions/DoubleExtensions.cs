namespace Testeger.Shared.Extensions;

public static class DoubleExtensions
{
    public static string ToHoursAndMinutes(this double hours)
    {
        int wholeHours = (int)hours;

        double fractionalHours = hours - wholeHours;
        int minutes = (int)(fractionalHours * 60);

        return $"{wholeHours}h {minutes}min";
    }
}
