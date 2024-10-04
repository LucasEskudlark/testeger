using Testeger.Client.ViewModels.TestCases;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Calculators;

public static class TestCaseMetricsCalculator
{
    public static double CalculateAverageTestCaseCompletionTimeInHours(IEnumerable<TestCaseViewModel> testCases)
    {
        var completedTestCases = testCases.Where(r => r.CompletedDate != default);

        if (!completedTestCases.Any())
        {
            return 0.0;
        }

        double totalHours = completedTestCases
            .Select(r => (r.CompletedDate - r.CreatedDate).TotalHours)
            .Sum();

        double averageHours = totalHours / completedTestCases.Count();

        return Math.Round(averageHours, 2);
    }

    public static double CalculateAverageTestCaseTestingTimeInHours(IEnumerable<TestCaseViewModel> testCases)
    {
        var testedTestCases = testCases.Where(r => r.Results.Count() > 1);

        if (!testedTestCases.Any())
        {
            return 0.0;
        }

        var allTimeSpans = testCases
            .SelectMany(tc => tc.Results)
            .Select(tcr => tcr.ElapsedTime)
            .ToList();

        var totalHoursSpent = allTimeSpans.Sum(ts => ts.TotalHours);

        double averageHours = totalHoursSpent / allTimeSpans.Count;

        return Math.Round(averageHours, 2);
    }

    public static int CountFailedTestCases(IEnumerable<TestCaseViewModel> testCases)
    {
        int count = testCases.Count(tc => tc.History.Any(h => h.NewStatus == TestCaseStatus.Failed));
        return count;
    }

    public static int CountNeverFailedTestCases(IEnumerable<TestCaseViewModel> testCases)
    {
        int count = testCases.Count(tc => !tc.History.Any(h => h.NewStatus == TestCaseStatus.Failed));
        return count;
    }

    public static IEnumerable<TestCaseViewModel> FilterTestCasesByDate(IEnumerable<TestCaseViewModel> testCases, DateTime? filterDate)
    {
        if (filterDate == null)
        {
            return testCases;
        }

        return testCases.Where(testCase =>
        {
            var mostRecentChangeDate = testCase.History
                .OrderByDescending(history => history.ChangedDate)
                .FirstOrDefault()?.ChangedDate;

            return mostRecentChangeDate.HasValue && mostRecentChangeDate.Value >= filterDate.Value;
        });
    }

    public static Dictionary<TestCaseStatus, TimeSpan> CalculateAverageTimeInEachStatus(IEnumerable<TestCaseViewModel> testRequests)
    {
        var totalTimeInStatus = new Dictionary<TestCaseStatus, TimeSpan>();
        var countInStatus = new Dictionary<TestCaseStatus, int>();

        foreach (var testRequest in testRequests)
        {
            var orderedHistory = testRequest.History.OrderBy(h => h.ChangedDate).ToList();

            for (int i = 0; i < orderedHistory.Count; i++)
            {
                var currentStatus = orderedHistory[i].NewStatus;
                var startTime = orderedHistory[i].ChangedDate;
                var endTime = i < orderedHistory.Count - 1 ? orderedHistory[i + 1].ChangedDate : DateTime.Now;
                var duration = endTime - startTime;

                if (!totalTimeInStatus.ContainsKey(currentStatus))
                {
                    totalTimeInStatus[currentStatus] = TimeSpan.Zero;
                    countInStatus[currentStatus] = 0;
                }

                totalTimeInStatus[currentStatus] += duration;
                countInStatus[currentStatus]++;
            }
        }

        var averageTimeInStatus = new Dictionary<TestCaseStatus, TimeSpan>();
        foreach (var status in totalTimeInStatus.Keys)
        {
            averageTimeInStatus[status] = TimeSpan.FromTicks(totalTimeInStatus[status].Ticks / countInStatus[status]);
        }

        return averageTimeInStatus;
    }
}
