using System.ComponentModel.DataAnnotations;

namespace w10d3.Models
{
    public class Job
    {
        public int Id { get; set; }

        public System.DateTime CreatedAt { get; set; }
        public System.DateTime UpdatedAt { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Rate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Hours { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Company { get; set; }

        public string creatorId { get; set; }
        public Profile Creator { get; set; }
    }
}