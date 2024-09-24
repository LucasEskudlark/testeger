using Testeger.Client.ViewModels.TestRequests;
using Testeger.Shared.DTOs.Enumerations;

namespace Testeger.Client.Calculators;

public static class TestRequestMetricsCalculator
{
    public static double CalculateAverageTestRequestCompletionTimeInHours(IEnumerable<TestRequestViewModel> testRequests)
    {
        var completedTestRequests = testRequests.Where(r => r.CompletedDate != DateTime.MinValue);

        if (!completedTestRequests.Any())
        {
            return 0.0;
        }

        double totalHours = completedTestRequests
            .Select(r => (r.CompletedDate - r.CreatedDate).TotalHours)
            .Sum();

        double averageHours = totalHours / completedTestRequests.Count();

        return Math.Round(averageHours, 2);
    }

    public static IEnumerable<TestRequestViewModel> FilterTestRequestsByDate(
        IEnumerable<TestRequestViewModel> requests, DateTime? filterDate)
    {
        if (filterDate == null)
        {
            return requests;
        }

        return requests.Where(request =>
        {
            var mostRecentChangeDate = request.History
                .OrderByDescending(history => history.ChangedDate)
                .FirstOrDefault()?.ChangedDate;

            return mostRecentChangeDate.HasValue && mostRecentChangeDate.Value >= filterDate.Value;
        });
    }

    public static double CalculateAverageRequestCompletionTimeInHours(IEnumerable<TestRequestViewModel> requests)
    {
        var completedRequests = requests.Where(r => r.CompletedDate != default);

        if (!completedRequests.Any())
        {
            return 0.0;
        }

        double totalHours = completedRequests
            .Select(r => (r.CompletedDate - r.CreatedDate).TotalHours)
            .Sum();

        double averageHours = totalHours / completedRequests.Count();

        return Math.Round(averageHours, 2);
    }

    public static int CountRequestsCompletedBeforeDueDate(IEnumerable<TestRequestViewModel> requests)
    {
        return requests.Count(r => r.CompletedDate != default &&
                                   r.CompletedDate < r.DueDate);
    }

    public static int CountRequestsCompletedAfterDueDate(IEnumerable<TestRequestViewModel> requests)
    {
        return requests.Count(r => r.CompletedDate != default &&
                                   r.CompletedDate > r.DueDate);
    }

    public static int CountRequestsThatNeededToBeFixed(IEnumerable<TestRequestViewModel> requests)
    {
        return requests.Count(tr => tr.History.Any(h => h.NewStatus == RequestStatus.FixingIssues));
    }

    public static int CountRequestsThatDidntNeedToBeFixed(IEnumerable<TestRequestViewModel> requests)
    {
        return requests.Count(tr => !tr.History.Any(h => h.NewStatus == RequestStatus.FixingIssues));
    }

    public static double CalculateAverageLeadTime(IEnumerable<TestRequestViewModel> testRequests)
    {
        if (!testRequests.Any())
        {
            return 0;
        }

        double totalLeadTime = 0;
        int count = 0;

        foreach (var request in testRequests)
        {
            var started = request.History
                .Where(h => h.NewStatus == RequestStatus.Waiting)
                .OrderBy(h => h.ChangedDate)
                .FirstOrDefault()?.ChangedDate;

            var completed = request.History
                .Where(h => h.NewStatus == RequestStatus.Completed && h.ChangedDate >= started)
                .OrderBy(h => h.ChangedDate)
                .FirstOrDefault()?.ChangedDate;

            if (started.HasValue && completed.HasValue)
            {
                totalLeadTime += (completed.Value - started.Value).TotalHours;
                count++;
            }
        }

        if (count > 0)
        {
            var result = totalLeadTime / count;
            return Math.Round(result, 2);
        }

        return 0;
    }
    public static double CalculateCycleTime(IEnumerable<TestRequestViewModel> testRequests)
    {
        if (!testRequests.Any())
        {
            return 0;
        }

        double totalCycleTime = 0;
        int count = 0;

        foreach (var request in testRequests)
        {
            var started = request.History
                .Where(h => h.NewStatus == RequestStatus.InProgress)
                .OrderBy(h => h.ChangedDate)
                .FirstOrDefault()?.ChangedDate;

            var completed = request.History
                .Where(h => h.NewStatus == RequestStatus.Completed && h.ChangedDate >= started)
                .OrderBy(h => h.ChangedDate)
                .FirstOrDefault()?.ChangedDate;

            if (started.HasValue && completed.HasValue)
            {
                totalCycleTime += (completed.Value - started.Value).TotalHours;
                count++;
            }
        }

        if (count > 0)
        {
            var result = totalCycleTime / count;
            return Math.Round(result, 2);
        }

        return 0;
    }
}
