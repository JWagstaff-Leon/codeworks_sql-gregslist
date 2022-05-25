using System.ComponentModel.DataAnnotations;
using w10d3.Models;

namespace w10d3.Models
{
    public class Car
    {
        public int Id { get; set; }

        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
        
        public string Color { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
}