namespace CarManager.Shared.Infrastructure.Time;

internal class UtcClock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}