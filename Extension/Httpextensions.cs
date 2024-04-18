using System.Text.Json;
using PublishWell.Common.Helper;

namespace PublishWell.Extension;

/// <summary>
/// Extension class for http response objects to add a "Pagination" header with pagination information.
/// </summary>
public static class HttpExtensions
{
    /// <summary>
    /// Adds a "Pagination" header to an HTTP response.
    /// </summary>
    /// <param name="response">The HttpResponse object to add the header to.</param>
    /// <param name="header">The PaginationHeader object containing pagination information.</param>
    public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
    {
        // Configure JSON serializer options for camelCase property naming
        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        // Serialize the PaginationHeader object to JSON using the options
        var json = JsonSerializer.Serialize(header, jsonOptions);

        // Add the "Pagination" header to the response with the serialized JSON data
        response.Headers.Append("Pagination", json);

        // Add an "Access-Control-Expose-Headers" header to allow client-side access to the "Pagination" header
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}
