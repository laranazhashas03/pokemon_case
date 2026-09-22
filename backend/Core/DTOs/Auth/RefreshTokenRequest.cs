namespace Core.DTOs.Auth;

// Represents the refresh token sent by the client
// when requesting a new access token.
public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}