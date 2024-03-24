using System.Text.Json.Serialization;

namespace ChaptersMobileApp.Models;

public class GetUserCommentResult
{
    public int Id { get; init; }
    [JsonPropertyName("authorId")]
    public int AuthorId { get; init; }
    [JsonPropertyName("authorUsername")]
    public string AuthorUsername { get; init; }
    public string Text { get; init; }
    public int Rating { get; init; }
    [JsonPropertyName("userRating")]
    public int UserRating { get; init; }
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; init; }
    [JsonPropertyName("chapterId")]
    public int ChapterId { get; init; }
    [JsonPropertyName("chapterTitle")]
    public string ChapterTitle { get; init; }
    [JsonPropertyName("bookId")]
    public int BookId { get; init; }
    [JsonPropertyName("bookTitle")]
    public string BookTitle { get; init; }
}