using MediatR;
using Planetary.Application.DTOs;
using Planetary.Application.Interfaces;
using Planetary.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Planetary.Application.Commands
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Organization { get; set; }
        public string? JobTitle { get; set; }
        public string? ResearchInterests { get; set; }
        public string? PreferredPlanetarySystem { get; set; }
        public UserType UserType { get; set; } = UserType.Viewer;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterUserAsync(
                request.Username, 
                request.Email, 
                request.Password, 
                request.FirstName, 
                request.LastName,
                request.Organization ?? string.Empty,
                request.JobTitle ?? string.Empty,
                request.ResearchInterests ?? string.Empty,
                request.PreferredPlanetarySystem ?? string.Empty,
                request.UserType);
        }
    }
}