using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength (500)]
        public string Description { get; set; }
        public byte[] Poster { get; set; }  
        public double Rating { get; set; }
        public double Price { get; set; }
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
