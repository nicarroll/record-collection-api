using api.Models;
using api.DTOs;

namespace api.Mappings;

public static class RecordCollectionMappings
{
    public static RecordResponse ToResponse(this RecordCollection record)
    {
        return new RecordResponse
        {
            Id = record.Id,
            ArtistName = record.ArtistName,
            AlbumTitle = record.AlbumTitle,
            ReleaseYear = record.ReleaseYear,
            DiscogsId = record.DiscogsId
        };
    }

    public static RecordCollection ToModel(this CreateRecordRequest request)
    {
        return new RecordCollection
        {
            ArtistName = request.ArtistName,
            AlbumTitle = request.AlbumTitle,
            ReleaseYear = request.ReleaseYear,
            DiscogsId = request.DiscogsId
        };
    }

    public static RecordCollection ToModelWithId(this UpdateRecordRequest request, int id)
    {
        return new RecordCollection
        {
            Id = id,
            ArtistName = request.ArtistName,
            AlbumTitle = request.AlbumTitle,
            ReleaseYear = request.ReleaseYear,
            DiscogsId = request.DiscogsId
        };
    }
}