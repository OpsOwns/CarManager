namespace CarManager.API.Requests.Authentication;

internal sealed record RefreshJsonWebTokenRequest(string AccessToken, string RefreshToken);