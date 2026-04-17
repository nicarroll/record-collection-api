namespace api.Models;
public class RecordCollection
{
    public int Id  { get; set; }
    public string ArtistName { get; set; } = "";

    public string AlbumTitle {get; set; } = "";

    public int ReleaseYear { get; set; }

    public long DiscogsId { get; set; }
}