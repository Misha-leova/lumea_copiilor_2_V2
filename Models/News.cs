using System;
using System.ComponentModel.DataAnnotations;

namespace lumea_copiilor_2.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        // Removed AuthorId and Author properties

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}