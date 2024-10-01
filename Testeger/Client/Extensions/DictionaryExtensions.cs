using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Extensions;

public static class DictionaryExtensions
{
    public static string GetRequestTimeByStatus(this Dictionary<RequestStatus, TimeSpan> dictionary, RequestStatus status)
    {
        if (dictionary.TryGetValue(status, out TimeSpan timeSpan))
        {
            return timeSpan.ToCustomFormat();
        }
        else
        {
            return new TimeSpan(0, 0, 0).ToCustomFormat();
        }
    }

    public static double GetRequestTimeByStatusForChart(this Dictionary<RequestStatus, TimeSpan> dictionary, RequestStatus status)
    {
        if (dictionary.TryGetValue(status, out TimeSpan timeSpan))
        {
            return Math.Round(timeSpan.TotalHours, 2);
        }
        else
        {
            return 0.0;
        }
    }
}
