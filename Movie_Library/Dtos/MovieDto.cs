using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations.Dtos
{
    public abstract class MovieDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public byte GenreId { get; set; }
    }
}
