using System;

namespace Planetary.Application.DTOs
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}