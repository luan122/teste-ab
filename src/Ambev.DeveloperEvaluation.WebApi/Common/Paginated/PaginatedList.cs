namespace Ambev.DeveloperEvaluation.WebApi.Common.Paginated;

/// <summary>
/// Represents a paginated collection of items with metadata about the pagination.
/// </summary>
/// <remarks>
/// Used as the standard response format for all paginated API endpoints.
/// </remarks>
public class PaginatedList<T> : List<T>
{
    /// <summary>
    /// Gets or sets the current page number being viewed.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the number of items displayed per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total number of items across all pages.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets a value indicating whether there is a previous page available.
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page available.
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    public PaginatedList() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class with the specified values and pagination information.
    /// </summary>
    /// <param name="values">The collection of items for the current page.</param>
    /// <param name="count">The total number of items across all pages.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginatedList(List<T> values, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(values);
    }
}
