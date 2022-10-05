namespace CRUD_Operations.Dtos
{
    public class CreateMovieDto : MovieDto
    {
        public IFormFile Poster { get; set; }
    }
}
