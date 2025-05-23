using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Service;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController(ArtistService service) : ControllerBase
{
    private readonly ArtistService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var artists = await _service.GetAllAsync();
        return Ok(artists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var artist = await _service.GetByIdAsync(id);
        return artist is not null ? Ok(artist) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Artists artist)
    {
        await _service.AddAsync(artist);
        return CreatedAtAction(nameof(GetById), new { id = artist.Id }, artist);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Artists updatedArtist)
    {
        var success = await _service.UpdateAsync(id, updatedArtist);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
