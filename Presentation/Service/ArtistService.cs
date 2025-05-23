using Microsoft.EntityFrameworkCore;
using Presentation.Contexts;
using Presentation.Models;

namespace Presentation.Service;

public class ArtistService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<List<Artists>> GetAllAsync()
    {
        return await _db.Artists.ToListAsync();
    }

    public async Task<Artists?> GetByIdAsync(string id)
    {
        return await _db.Artists.FindAsync(id);
    }

    public async Task AddAsync(Artists artist)
    {
        _db.Artists.Add(artist);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(string id, Artists updatedArtist)
    {
        var artist = await _db.Artists.FindAsync(id);
        if (artist is null) return false;

        artist.Name = updatedArtist.Name;
        artist.Description = updatedArtist.Description;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var artist = await _db.Artists.FindAsync(id);
        if (artist is null) return false;

        _db.Artists.Remove(artist);
        await _db.SaveChangesAsync();
        return true;
    }
}
