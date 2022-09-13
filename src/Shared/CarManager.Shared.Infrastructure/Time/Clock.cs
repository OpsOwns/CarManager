namespace CarManager.Shared.Infrastructure.Time;

internal sealed class UtcClock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}