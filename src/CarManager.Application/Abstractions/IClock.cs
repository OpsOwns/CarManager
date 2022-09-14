namespace CarManager.Shared.Abstractions.Common;

public interface IClock
{
    DateTime Now { get; }
}