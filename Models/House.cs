using System.ComponentModel.DataAnnotations;

namespace w10d3.Models
{
    public class House
    {
        public int Id { get; set; }

        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Bathrooms { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Bedrooms { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Levels { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public string creatorId { get; set; }
        public Profile Creator { get; set; }
    }
}