namespace CarManager.API.Requests.Authentication;

public record RefreshJsonWebTokenRequest(string AccessToken, string RefreshToken);