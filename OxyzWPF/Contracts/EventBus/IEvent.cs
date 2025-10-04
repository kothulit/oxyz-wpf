namespace OxyzWPF.Contracts.EventBus;

/// <summary>
/// Базовый интерфейс для всех событий
/// </summary>
public interface IEvent
{
    DateTime Timestamp { get; }
    string EventType { get; }
}