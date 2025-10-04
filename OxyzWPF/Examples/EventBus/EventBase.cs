using OxyzWPF.Contracts.EventBus;

namespace OxyzWPF.EventBus;

/// <summary>
/// Базовый класс для событий с данными
/// </summary>
public abstract class EventBase : IEvent
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
    public abstract string EventType { get; }
}