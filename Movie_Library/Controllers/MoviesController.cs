using CRUD_Operations.Dtos;
using CRUD_Operations.Models;
using CRUD_Operations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices _movieServices;
        public MoviesController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var movies = await _movieServices.GetAllMoviesAsync();

            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieAsync(int id)
        {
            var movie = await _movieServices.GetMovieAsync(id);
            if (movie.Id == 0)
                return NotFound("NOT Found Movie");

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieAsync([FromForm] CreateMovieDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = await _movieServices.CreateMovieAsync(dto);
            if (movie.Id == 0)
                return BadRequest("Check Your Data Sent");

            return Ok(movie);
        }
        [HttpGet("GetMoviesGenre")]
        public async Task<IActionResult> GetMoviesGenreAsync(byte genreid)
        {
            var movies = await _movieServices.GetMoviesGenreAsync(genreid);

            return Ok(movies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieAsync(int id, [FromForm] UpdateMovieDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = await _movieServices.UpdateMovieAsync(id, dto);

            if (movie.Id == 0)
                return BadRequest("Check Your Data Sent");

            return Ok(movie);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            var movie = await _movieServices.DeleteMovieAsync(id);

            if (movie.Id == 0)
                return NotFound($"NOT Found Id {id}");

            return Ok(movie);
        }
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllMoviesAsync()
        {
            var result = await _movieServices.DeleteAllMoviesAsync();
            return Ok(result);
        }
    }
}
