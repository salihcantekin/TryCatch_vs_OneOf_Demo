namespace OneOf.WebApi.Models;

public record UserLoginResponse(string FullName, string AccessToken, string RefreshToken);
