using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using api.Services;
using api.Models;


namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordCollectionController : ControllerBase
{
    private readonly IRecordCollectionService _service;

    public RecordCollectionController(IRecordCollectionService service)
    {
        _service = service;
    }
    
    //Get ALL records
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var records = await _service.GetAllAsync();
        return Ok(records);
    }

    //Get record by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var record = await _service.GetByIdAsync(id);
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
        
        if (record == null)
        {
            return BadRequest("Record data is required.");
        }

        if (string.IsNullOrEmpty(record.ArtistName) || 
        string.IsNullOrEmpty(record.AlbumTitle))
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
        
        var created = await _service.CreateAsync(record);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        
        
        
    }
    
    //Delete a record from the collection
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
            var deleted = await _service.DeleteAsync(id);            

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();

    }   



    //Update a record in the collection
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RecordCollection record)
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

            var updated = await _service.UpdateAsync(id, record);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();

    }

}