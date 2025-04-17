namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Represents a standardized API response structure that includes a data payload.
/// </summary>
/// <typeparam name="T">The type of data returned in the response.</typeparam>
/// <remarks>
/// Extends the base ApiResponse class by adding support for returning data alongside status information.
/// </remarks>
public class ApiResponseWithData<T> : ApiResponse
{
    /// <summary>
    /// Gets or sets the data payload returned by the API.
    /// </summary>
    /// <remarks>
    /// This property contains the actual result of the API operation when successful.
    /// It may be null in case of failed operations.
    /// </remarks>
    public T? Data { get; set; }
}
