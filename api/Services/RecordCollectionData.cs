using Dapper;
using Npgsql;
using api.Models;

namespace api.Services;


public class RecordCollectionData : IRecordCollectionService
{
    private readonly IConfiguration _configuration;

    public RecordCollectionData(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<RecordCollection>> GetAllAsync()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        await using var conn = new NpgsqlConnection(connectionString);


        var sql = @"SELECT id, artist_name as ArtistName, album_title as AlbumTitle, 
            release_year as ReleaseYear, discogs_id as DiscogsId FROM records ORDER BY id DESC";

        return await conn.QueryAsync<RecordCollection>(sql);
    }

    public async Task<RecordCollection?> GetByIdAsync(int id)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection"); 
        await using var conn = new NpgsqlConnection(connectionString);

        var sql = @"SELECT id, artist_name as ArtistName, album_title as AlbumTitle, 
            release_year as ReleaseYear, discogs_id as DiscogsId FROM records WHERE id = @id";

        return await conn.QuerySingleOrDefaultAsync<RecordCollection>(sql, new { id });
    }

    public async Task<RecordCollection> CreateAsync(RecordCollection record)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection"); 
        await using var conn = new NpgsqlConnection(connectionString);

        var sql = @"
            INSERT INTO records (artist_name, album_title, release_year, discogs_id)
            VALUES (@ArtistName, @AlbumTitle, @ReleaseYear, @DiscogsId)
            RETURNING id;";

        var newId = await conn.ExecuteScalarAsync<int>(sql, record);
        record.Id = newId;

        return record;
    }

    public async Task<bool> UpdateAsync(int id, RecordCollection record)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection"); 
        await using var conn = new NpgsqlConnection(connectionString);

        var sql = @"
                UPDATE records
                SET artist_name = @ArtistName, album_title = @AlbumTitle, release_year = @ReleaseYear, discogs_id = @DiscogsId
                WHERE id = @Id";

            return await conn.ExecuteAsync(sql, new { Id = id, record.ArtistName, record.AlbumTitle,
             record.ReleaseYear, record.DiscogsId }) > 0;    
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection"); 
        await using var conn = new NpgsqlConnection(connectionString);

        var sql = "DELETE FROM records WHERE id = @id";

        return await conn.ExecuteAsync(sql, new { id }) > 0;
    }
}
