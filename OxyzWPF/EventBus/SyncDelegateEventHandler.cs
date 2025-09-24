using OxyzWPF.Contracts.EventBus;

namespace OxyzWPF.EventBus;

/// <summary>
/// Обертка для синхронного делегата
/// </summary>
internal class SyncDelegateEventHandler<TEvent> : ISyncEventHandler<TEvent> where TEvent : IEvent
{
    private readonly Action<TEvent> _handler;

    public SyncDelegateEventHandler(Action<TEvent> handler)
    {
        _handler = handler;
    }

    public void Handle(TEvent eventData)
    {
        _handler(eventData);
    }
}
