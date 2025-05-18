using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lumea_copiilor_2.Models
{
    public class UserInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserInformationId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; } // Nullable for optional field

        [Required]
        public bool SubscribedToNewsletter { get; set; } = false; // Default to not subscribed

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Automatically updated on changes

        // Additional fields can be added as needed
        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }
    }
}