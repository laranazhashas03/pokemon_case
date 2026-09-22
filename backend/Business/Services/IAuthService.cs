using Core.DTOs.Auth;

namespace Business.Services;

// Defines authentication operations for users.
public interface IAuthService
{
    // Registers a new user and returns authentication information.
    Task<AuthResponse> RegisterAsync(RegisterRequest request);

    // Authenticates a user and returns authentication information.
    Task<AuthResponse> LoginAsync(LoginRequest request);
}