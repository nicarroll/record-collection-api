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

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        try 
        {

            var sql = @"SELECT id, artist_name as ArtistName, album_title as AlbumTitle,
             release_year as ReleaseYear, discogs_id as DiscogsId FROM records ORDER BY id DESC";

            var records = await conn.QueryAsync<RecordCollection>(sql);

            return Ok(records);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving records.");
        }
    }


    //Get record by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        try
        {
            var sql = @"SELECT id, artist_name as ArtistName, album_title as AlbumTitle, 
            release_year as ReleaseYear, discogs_id as DiscogsId FROM records WHERE id = @id";

            var record = await conn.QuerySingleOrDefaultAsync<RecordCollection>(sql, new { id });

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving the record.");
        }
    }


    //Add a new record to the collection
    [HttpPost]
    public async Task<IActionResult> Post(RecordCollection record)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        

        await using var conn = new NpgsqlConnection(connectionString);

        try
        {
            if (record == null)
            {
                return BadRequest("Record data is required.");
            }

            if (string.IsNullOrEmpty(record.ArtistName) || string.IsNullOrEmpty(record.AlbumTitle))
            {
                return BadRequest("Artist name and album title are required.");
            }

            if (record.ReleaseYear <= 0)
            {
                return BadRequest("Release year must be a positive integer.");
            }

            if (record.DiscogsId <= 0)
            {
                return BadRequest("Discogs ID must be a positive integer.");
            }

            await conn.OpenAsync();

            var sql = @"
            INSERT INTO records (artist_name, album_title, release_year, discogs_id)
            VALUES (@artist_name, @album_title, @release_year, @discogs_id)
            RETURNING id;";

            var newId = await conn.ExecuteScalarAsync<int>(sql, record);

            record.Id = newId;
            
            return CreatedAtAction(nameof(GetById), new { id = newId }, record);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, "An error occurred while inserting new record data.");
        }
        
    }
    
    //Delete a record from the collection
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        try
        {
            var sql = "DELETE FROM records WHERE id = @id";

            var rowsAffected = await conn.ExecuteAsync(sql, new { id });

            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return NoContent();

        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the record.");
        }
    }


    //Update a record in the collection
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RecordCollection record)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection"); 

        await using var conn = new NpgsqlConnection(connectionString);

        try
        {
            if (record == null)
            {
                return BadRequest("Record data is required.");
            }

            if (string.IsNullOrEmpty(record.ArtistName) || string.IsNullOrEmpty(record.AlbumTitle))
            {
                return BadRequest("Artist name and album title are required.");
            }

            if (record.ReleaseYear <= 0)
            {
                return BadRequest("Release year must be a positive integer.");
            }

            if (record.DiscogsId <= 0)
            {
                return BadRequest("Discogs ID must be a positive integer.");
            }

            await conn.OpenAsync();            

            var sql = @"
                UPDATE records
                SET artist_name = @artist_name, album_title = @album_title, release_year = @release_year, discogs_id = @discogs_id
                WHERE id = @id";

            var rowsAffected = await conn.ExecuteAsync(sql, new {
                id,
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
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the record.");
        }

    }

}