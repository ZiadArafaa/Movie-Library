using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
