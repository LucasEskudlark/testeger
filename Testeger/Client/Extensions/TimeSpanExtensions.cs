namespace Testeger.Client.Extensions;

public static class TimeSpanExtensions
{
    public static string ToCustomFormat(this TimeSpan timeSpan)
    {
        int days = timeSpan.Days;
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;

        return $"{days}d {hours}h {minutes}min";
    }
}
