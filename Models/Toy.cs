using System;
using System.ComponentModel.DataAnnotations;

namespace lumea_copiilor_2.Models
{
    public class Toy
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsSpecialOffer { get; set; }  // If the toy is in a special offer

        public DateTime? SpecialOfferEndDate { get; set; } // Optional end date for the special offer

        [Required]
        public string Category { get; set; } // For example: "Building blocks", "Cars"
    }
}
