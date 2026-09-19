using Business.Helpers;
using Core.DTOs.Auth;
using DataAccess.UnitOfWork;
using Entity.Entities;
using Core.Settings;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHashService _passwordHashService;
    private readonly JwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
    IUnitOfWork unitOfWork,
    PasswordHashService passwordHashService,
    JwtTokenService jwtTokenService,
    IOptions<JwtSettings> jwtSettings)
{
    _unitOfWork = unitOfWork;
    _passwordHashService = passwordHashService;
    _jwtTokenService = jwtTokenService;
    _jwtSettings = jwtSettings.Value;
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

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = await CreateAndSaveRefreshTokenAsync(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken
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

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = await CreateAndSaveRefreshTokenAsync(user);

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private async Task<string> CreateAndSaveRefreshTokenAsync(User user)
{
    var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();

    var refreshToken = new RefreshToken
    {
        Token = refreshTokenValue,
        UserId = user.Id,
        ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
    };

    await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
    await _unitOfWork.SaveChangesAsync();

    return refreshTokenValue;
}
}