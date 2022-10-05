namespace CRUD_Operations.Dtos
{
    public class UpdateMovieDto : MovieDto
    {
        public IFormFile? Poster { get; set; }
    }
}
