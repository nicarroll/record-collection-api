using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;


[ApiController]
[Route("api/[controller]")]
public class RecordCollectionController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public RecordCollectionController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    //Get ALL records
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        var records = new List<RecordCollection>();

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var cmd = new NpgsqlCommand("SELECT id, artist_name, album_title, release_year, discogs_id FROM records", conn);

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var record = new RecordCollection
            {
                Id = reader.GetInt32(0),
                ArtistName = reader.GetString(1),
                AlbumTitle = reader.GetString(2),
                ReleaseYear = reader.GetInt32(3),
                DiscogsId = reader.GetInt64(4)
            };

            records.Add(record);
        }

        return Ok(records);
    }


    //Get record by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var sql = "SELECT id, artist_name as ArtistName, album_title as AlbumTitle, release_year as ReleaseYear, discogs_id as DiscogsId FROM records WHERE id = @id";

        var record = await conn.QuerySingleOrDefaultAsync<RecordCollection>(sql, new { id });

        if (record == null)
        {
            return NotFound();
        }

        return Ok(record);
    }


    //Add a new record to the collection
    [HttpPost]
    public async Task<IActionResult> Post(RecordCollection record)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var sql = @"
            INSERT INTO records (artist_name, album_title, release_year, discogs_id)
            VALUES (@artist_name, @album_title, @release_year, @discogs_id)
            RETURNING id;";

        await using var cmd = new NpgsqlCommand(sql, conn);
        
        cmd.Parameters.AddWithValue("artist_name", record.ArtistName);
        cmd.Parameters.AddWithValue("album_title", record.AlbumTitle);
        cmd.Parameters.AddWithValue("release_year", record.ReleaseYear);
        cmd.Parameters.AddWithValue("discogs_id", record.DiscogsId);

        var newId = (int)(await cmd.ExecuteScalarAsync())!;

        record.Id = newId;
        
        return Ok(record);
    }
    
    //Delete a record from the collection
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var sql = "DELETE FROM records WHERE id = @id";

        var rowsAffected = await conn.ExecuteAsync(sql, new { id });

        if (rowsAffected == 0)
        {
            return NotFound();
        }

        return NoContent();
    }


    //Update a record in the collection
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RecordCollection record)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection"); 

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var sql = @"
            UPDATE records
            SET artist_name = @artist_name, album_title = @album_title, release_year = @release_year, discogs_id = @discogs_id
            WHERE id = @id";

        var rowsAffected = await conn.ExecuteAsync(sql, new {
            artist_name = record.ArtistName,
            album_title = record.AlbumTitle,
            release_year = record.ReleaseYear,
            discogs_id = record.DiscogsId
        });

        if (rowsAffected == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

}