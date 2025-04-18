using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Represents a standardized API response structure returned by all endpoints.
/// </summary>
/// <remarks>
/// Provides consistent response format throughout the API.
/// </remarks>
public class ApiResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the API request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message providing additional information about the API response.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a collection of validation errors that occurred during request processing.
    /// </summary>
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
public class ApiResponse<T>
{
    /// <summary>
    /// Gets or sets a value indicating whether the API request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message providing additional information about the API response.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a collection of validation errors that occurred during request processing.
    /// </summary>
    public T? Errors { get; set; } = default;
}