using CRUD_Operations.Data;
using CRUD_Operations.Dtos;
using CRUD_Operations.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _db;
        private readonly long _sizefile= 1048576;
        private readonly List<string> _allowExtentions = new List<string> { ".png", ".jpg" };
        public MovieServices(ApplicationDbContext db )
        {
            _db = db;
        }

        public async Task<Movie> CreateMovieAsync(CreateMovieDto dto)
        {

            if(await _db.movies.AnyAsync(m=>m.Name==dto.Name))
                return new Movie();
            if (dto.Poster.Length > _sizefile)
                return new Movie();
            if (!_allowExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return new Movie();
            if (!await _db.genres.AnyAsync(g => g.Id == dto.GenreId))
                return new Movie();

            using var fileposter = new MemoryStream();
            await dto.Poster.CopyToAsync(fileposter);


            var movie = new Movie
            {
                Name = dto.Name,
                Description = dto.Description,
                GenreId=dto.GenreId,
                Poster=fileposter.ToArray(),
                Rating=dto.Rating,
                Price=dto.Price,
            };

            await _db.movies.AddAsync(movie);
            await _db.SaveChangesAsync();


            return movie;
        }
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            var movies = await _db.movies.Include(m => m.Genre).ToListAsync();

            return movies;
        }
        public async Task<Movie> GetMovieAsync(int id)
        {
            if (!await _db.movies.AnyAsync(m => m.Id == id))
                return new Movie();

            var movie = await _db.movies.Include(m=>m.Genre).SingleOrDefaultAsync(m=>m.Id==id);

            return movie;
        }
        public async Task<List<Movie>> GetMoviesGenreAsync(byte id)
        {
            if (!await _db.genres.AnyAsync(g => g.Id == id))
                return new List<Movie>();

            var movies = await _db.movies.Include(g => g.Genre).Where(g => g.GenreId == id).ToListAsync();    

            return movies;
        }
        public async Task<Movie> UpdateMovieAsync(int id,UpdateMovieDto dto)
        {
            if (!await _db.movies.AnyAsync(m => m.Id == id))
                return new Movie();

            if (await _db.movies.AnyAsync(m => m.Name== dto.Name))
                return new Movie();

            if (!await _db.genres.AnyAsync(g => g.Id == dto.GenreId))
                return new Movie();

            var movie = await _db.movies.FindAsync(id);

            movie.Description = dto.Description;
            movie.GenreId = dto.GenreId;
            movie.Name = dto.Name;
            movie.Price = dto.Price;
            movie.Rating = dto.Rating;
            if(dto.Poster!=null)
            {
                if(!_allowExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return new Movie();

                if(dto.Poster.Length>_sizefile)
                    return new Movie();

                using var fileposter = new MemoryStream();
                await dto.Poster.CopyToAsync(fileposter);

                movie.Poster = fileposter.ToArray();
            }

            await _db.SaveChangesAsync();

            return movie;
        }
        public async Task<Movie> DeleteMovieAsync(int id)
        {
            if(!await _db.movies.AnyAsync(m => m.Id == id)) 
                return new Movie();

            var movie= await _db.movies.FindAsync(id);

            _db.movies.Remove(movie);
            await _db.SaveChangesAsync();

            return movie;
        }
        public async Task<string> DeleteAllMoviesAsync()
        {
            var movies = await _db.movies.ToListAsync();

            _db.movies.RemoveRange(movies);
            await _db.SaveChangesAsync();

            return "All Movies DElETED !";
        }
    }
}
