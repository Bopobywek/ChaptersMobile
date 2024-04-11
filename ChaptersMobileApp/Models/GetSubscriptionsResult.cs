using System.Text.Json.Serialization;

namespace ChaptersMobileApp.Models;

public class GetSubscriptionsResult
{
    public int Id { get; init; }
    [JsonPropertyName("userId")]

    public int UserId { get; init; }
    public string Username { get; init; }
    [JsonPropertyName("numberOfBooks")]
    public int NumberOfBooks { get; init; }
}