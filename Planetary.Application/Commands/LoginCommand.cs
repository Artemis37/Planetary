using MediatR;
using Planetary.Application.DTOs;
using Planetary.Application.Interfaces;

namespace Planetary.Application.Commands
{
    public class LoginCommand : IRequest<LoginResponse?>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse?>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var (success, user) = await _authService.ValidateUserAsync(request.Username, request.Password);

            if (!success || user == null)
            {
                return null;
            }

            return await _authService.GetLoginResponseAsync(user);
        }
    }
}