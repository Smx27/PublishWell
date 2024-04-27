namespace PublishWell.API.Common.Helper;

/// <summary>
/// Represents parameters for pagination used in API requests.
/// </summary>
public class PaginationParams
{
    /// <summary>
    /// The maximum allowed page size for API requests.
    /// </summary>
    private const int _maxPageSize = 50;

    /// <summary>
    /// The current page number (1-based). Defaults to 1.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// The number of items requested per page.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
    }

    private int _pageSize = 10; // Default page size
}
