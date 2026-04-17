using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class CreateRecordRequest
{
    [Required]
    [MaxLength(200)]
    public string ArtistName { get; set; } = "";
    [Required]
    [MaxLength(200)]
    public string AlbumTitle { get; set; } = "";
    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }
    [Range(1, long.MaxValue, ErrorMessage = "Discogs ID must be a positive integer.")]
    public long DiscogsId { get; set; }
}

public class UpdateRecordRequest
{
    [Required]
    [MaxLength(200)]
    public string ArtistName { get; set; } = "";
    [Required]
    [MaxLength(200)]
    public string AlbumTitle { get; set; } = "";
    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }
    [Range(1, long.MaxValue, ErrorMessage = "Discogs ID must be a positive integer.")]
    public long DiscogsId { get; set; }
}

public class RecordResponse
{
    public int Id { get; set; }
    public string ArtistName { get; set; } = "";
    public string AlbumTitle { get; set; } = "";
    public int ReleaseYear { get; set; }
    public long DiscogsId { get; set; }
}