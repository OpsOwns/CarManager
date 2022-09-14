﻿namespace CarManager.Infrastructure.Time;

internal sealed class UtcClock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}