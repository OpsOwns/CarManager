namespace CarManager.API.Requests.Authentication;

public sealed record RefreshJsonWebTokenRequest(string AccessToken, string RefreshToken);