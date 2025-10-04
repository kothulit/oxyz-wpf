namespace OxyzWPF.Contracts.EventBus;

/// <summary>
/// Интерфейс для обработчиков событий
/// </summary>
/// <typeparam name="TEvent">Тип события</typeparam>
public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent eventData);
}

/// <summary>
/// Синхронный обработчик событий
/// </summary>
/// <typeparam name="TEvent">Тип события</typeparam>
public interface ISyncEventHandler<in TEvent> where TEvent : IEvent
{
    void Handle(TEvent eventData);
}