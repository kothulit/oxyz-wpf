using OxyzWPF.Contracts.EventBus;

namespace OxyzWPF.EventBus;

/// <summary>
/// Обертка для асинхронного делегата
/// </summary>
public class DelegateEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
{
    private readonly Func<TEvent, Task> _handler;

    public DelegateEventHandler(Func<TEvent, Task> handler)
    {
        _handler = handler;
    }

    public Task HandleAsync(TEvent eventData)
    {
        return _handler(eventData);
    }
}
