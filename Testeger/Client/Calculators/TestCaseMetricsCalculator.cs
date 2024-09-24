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
}
