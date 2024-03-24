using System.Text.Json.Serialization;

namespace ChaptersMobileApp.Models;

public class GetUserActivityResult
{
    public int Id { get; init; }
    [JsonPropertyName("userId")]
    public int UserId { get; init; }
    public string Username { get; init; }
    public string Text { get; init; }
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }
}