namespace Testeger.Shared.DTOs.Responses;

public class PagedResponse<T>
{
    public required IEnumerable<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
