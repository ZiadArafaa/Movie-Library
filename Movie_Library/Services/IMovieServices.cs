using CRUD_Operations.Dtos;
using CRUD_Operations.Models;

namespace CRUD_Operations.Services
{
    public interface IMovieServices
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<List<Movie>> GetMoviesGenreAsync(byte id);
        Task<Movie> GetMovieAsync(int id); 
        Task<Movie> CreateMovieAsync(CreateMovieDto dto);
        Task<Movie> UpdateMovieAsync(int id,UpdateMovieDto dto);
        Task<Movie> DeleteMovieAsync(int id);
        Task<string> DeleteAllMoviesAsync();
    }
}
