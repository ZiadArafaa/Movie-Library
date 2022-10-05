using CRUD_Operations.Data;
using CRUD_Operations.Dtos;
using CRUD_Operations.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly ApplicationDbContext _db;
        public GenreServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Genre> CreateGenreAsync(GenreDto dto)
        {
            if (await _db.genres.AnyAsync(m => m.Name == dto.Name))
                return new Genre();

            var genre = new Genre
            {
                Name = dto.Name,
            };

            await _db.genres.AddAsync(genre);
            await _db.SaveChangesAsync();

            return genre;
        }

        public async Task<List<Genre>> GetAllGenreAsync()
        {
            var genres = await _db.genres.OrderBy(g=>g.Id).ToListAsync();

            return genres;
        }

        public async Task<Genre> UpdateGenreAsync(byte id, GenreDto dto)
        {
            if (await _db.genres.AnyAsync(m => m.Name == dto.Name)|| !await _db.genres.AnyAsync(g => g.Id == id))
                return new Genre();

            var genre = await _db.genres.FindAsync(id);

            genre.Name = dto.Name;

            await _db.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre> DeleteGenreAsync(byte id)
        {
            if (!await _db.genres.AnyAsync(g=>g.Id==id))
                return new Genre();

            var genre = await _db.genres.FindAsync(id);
 
            _db.genres.Remove(genre);
            
            await _db.SaveChangesAsync();

            return genre;
        }
    }
}
