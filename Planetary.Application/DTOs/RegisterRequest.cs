using System.ComponentModel.DataAnnotations;
using Planetary.Domain.Models;

namespace Planetary.Application.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? Organization { get; set; }
        
        public string? JobTitle { get; set; }
        
        public string? ResearchInterests { get; set; }
        
        public string? PreferredPlanetarySystem { get; set; }

        // Default to Viewer, but allow other types if specified
        public UserType UserType { get; set; } = UserType.Viewer;
    }
}