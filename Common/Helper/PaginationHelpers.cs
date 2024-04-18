namespace PublishWell.Common.Helper;

/// <summary>
/// Represents information about pagination for an API response.
/// </summary>
public class PaginationHeader
{
    /// <summary>
    /// Initializes a new instance of the PaginationHeader class.
    /// </summary>
    /// <param name="currentPage">The current page number (1-based).</param>
    /// <param name="itemsPerPage">The number of items displayed per page.</param>
    /// <param name="totalItems">The total number of items in the data source.</param>
    /// <param name="totalPages">The total number of pages available.</param>
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItem = totalItems;
        TotalPages = totalPages;
    }

    /// <summary>
    /// The current page number (1-based).
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The number of items displayed per page.
    /// </summary>
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// The total number of items in the data source (may be a typo, consider renaming to TotalCount).
    /// </summary>
    public int TotalItem { get; set; }

    /// <summary>
    /// The total number of pages available in the data source.
    /// </summary>
    public int TotalPages { get; set; }
}
