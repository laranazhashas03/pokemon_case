using Business.Helpers;
using Core.DTOs.Auth;
using DataAccess.UnitOfWork;
using Entity.Entities;

namespace Business.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHashService _passwordHashService;

    public AuthService(
        IUnitOfWork unitOfWork,
        PasswordHashService passwordHashService)
    {
        _unitOfWork = unitOfWork;
        _passwordHashService = passwordHashService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _unitOfWork.Users
            .GetByEmailAsync(request.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = string.Empty
        };

        user.PasswordHash = _passwordHashService
            .HashPassword(user, request.Password);

        await _unitOfWork.Users.AddAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _unitOfWork.Users
            .GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        var passwordValid = _passwordHashService.VerifyPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (!passwordValid)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email
        };
    }
}