using OxyzWPF.Contracts.EventBus;

namespace OxyzWPF.EventBus;

/// <summary>
/// Интерфейс EventBus
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Подписывается на событие
    /// </summary>
    void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

    /// <summary>
    /// Подписывается на событие с помощью делегата
    /// </summary>
    void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent;

    /// <summary>
    /// Подписывается на событие синхронно
    /// </summary>
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;

    /// <summary>
    /// Отписывается от события
    /// </summary>
    void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

    /// <summary>
    /// Публикует событие синхронно
    /// </summary>
    void Publish<TEvent>(TEvent eventData) where TEvent : IEvent;

    /// <summary>
    /// Публикует событие
    /// </summary>
    Task PublishAsync<TEvent>(TEvent eventData) where TEvent : IEvent;

    /// <summary>
    /// Публикует событие с отложенным выполнением
    /// </summary>
    void PublishLater<TEvent>(TEvent eventData, TimeSpan delay) where TEvent : IEvent;
}