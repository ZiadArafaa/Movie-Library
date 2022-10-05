using CRUD_Operations.Dtos;
using CRUD_Operations.Models;
using CRUD_Operations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreServices;
        public GenresController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenreAsync()
        {
            var genres=await _genreServices.GetAllGenreAsync();

            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenreAsync(GenreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre=await _genreServices.CreateGenreAsync(dto);

            if (genre.Id == 0)
                return BadRequest($"Genre {dto.Name} Already Exist !");
            
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreAsync(byte id, GenreDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = await _genreServices.UpdateGenreAsync(id,dto);

            if (genre.Id == 0)
                return BadRequest("Genre Dublicated or ID Not Found");

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreAsync(byte id)
        {
            var genre=await _genreServices.DeleteGenreAsync(id);

            if(genre.Id==0)
                return NotFound("Not Found ID");

            return Ok(genre);
        }
    }
}
