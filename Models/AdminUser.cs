
    using System;
    using System.ComponentModel.DataAnnotations;
    namespace lumea_copiilor_2.Models

{

    public class AdminUser
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; } 
    }
}