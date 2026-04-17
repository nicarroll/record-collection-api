using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.DTOs;
using api.Mappings;


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

        var response = records.Select(r => r.ToResponse())
        .ToList();

        return Ok(response);
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

        var response = record.ToResponse();
        return Ok(response);
    }


    //Add a new record to the collection
    [HttpPost]
    public async Task<IActionResult> Post(CreateRecordRequest request)
    {
        var record = request.ToModel();
        var created = await _service.CreateAsync(record);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
    }

    
    //Update a record in the collection
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateRecordRequest request)
    {
 
        var record = request.ToModelWithId(id);
        var updated = await _service.UpdateAsync(record);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();

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


}