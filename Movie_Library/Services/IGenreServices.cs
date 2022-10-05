using CRUD_Operations.Dtos;
using CRUD_Operations.Models;

namespace CRUD_Operations.Services
{
    public interface IGenreServices
    {
        Task<Genre> CreateGenreAsync(GenreDto dto);
        Task<List<Genre>> GetAllGenreAsync();
        Task<Genre> UpdateGenreAsync(byte id , GenreDto dto);
        Task<Genre> DeleteGenreAsync(byte id );
    }
}
