using Planetary.Application.DTOs;
using Planetary.Domain.Models;

namespace Planetary.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, User? User)> ValidateUserAsync(string username, string password);
        Task<string> GenerateJwtToken(User user);
        Task<LoginResponse> GetLoginResponseAsync(User user);
        Task<RegisterResponse> RegisterUserAsync(
            string username, 
            string email, 
            string password, 
            string firstName, 
            string lastName,
            string organization,
            string jobTitle,
            string researchInterests,
            string preferredPlanetarySystem,
            UserType userType);
    }
}