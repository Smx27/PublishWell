using Microsoft.EntityFrameworkCore;

namespace PublishWell.API.Common.Helper;

/// <summary>
/// Represents a collection of data with pagination information.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
public class PagedList<T> : List<T>
{
    /// <summary>
    /// Initializes a new instance of the PagedList class.
    /// </summary>
    /// <param name="items">The collection of items for this page.</param>
    /// <param name="count">The total number of items in the data source.</param>
    /// <param name="pageNumber">The current page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;

        // Add the provided items to the internal List collection
        AddRange(items);
    }

    /// <summary>
    /// The current page number (1-based).
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// The total number of pages available in the data source.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The number of items displayed per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The total number of items in the data source.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Creates a PagedList instance asynchronously from an IQueryable source.
    /// </summary>
    /// <param name="source">The IQueryable data source to paginate.</param>
    /// <param name="pageNumber">The desired page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A PagedList instance containing the requested page data and pagination information.</returns>
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        // Get the total count of items in the source asynchronously
        var count = await source.CountAsync();

        // Skip to the desired page and take the specified number of items asynchronously
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        // Create a new PagedList instance with the retrieved data and pagination information
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
