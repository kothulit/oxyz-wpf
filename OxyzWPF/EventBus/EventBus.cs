using OxyzWPF.Contracts.EventBus;
using System.Collections.Concurrent;

namespace OxyzWPF.EventBus;

/// <summary>
/// Реализация EventBus
/// </summary>
public class EventBus : IEventBus
{
    private readonly ConcurrentDictionary<Type, List<object>> _handlers = new();
    private readonly ConcurrentQueue<(IEvent eventData, DateTime executeAt)> _delayedEvents = new();
    private readonly Timer _delayedEventTimer;
    private readonly object _lock = new();

    public EventBus()
    {
        // Таймер для обработки отложенных событий
        _delayedEventTimer = new Timer(ProcessDelayedEvents, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
    }

    public void Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        _handlers.AddOrUpdate(
            eventType,
            new List<object> { handler },
            (key, existing) =>
            {
                lock (_lock)
                {
                    if (!existing.Contains(handler))
                        existing.Add(handler);
                    return existing;
                }
            });
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        var wrapper = new DelegateEventHandler<TEvent>(handler);
        Subscribe<TEvent>(wrapper);
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
    }

    //public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    //{
    //    var wrapper = new SyncDelegateEventHandler<TEvent>(handler);
    //    Subscribe<TEvent>(wrapper);
    //}

    public void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            lock (_lock)
            {
                handlers.Remove(handler);
                if (handlers.Count == 0)
                {
                    _handlers.TryRemove(eventType, out _);
                }
            }
        }
    }

    public async Task PublishAsync<TEvent>(TEvent eventData) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            var tasks = new List<Task>();

            lock (_lock)
            {
                foreach (var handler in handlers.ToList())
                {
                    if (handler is IEventHandler<TEvent> asyncHandler)
                    {
                        tasks.Add(asyncHandler.HandleAsync(eventData));
                    }
                    else if (handler is ISyncEventHandler<TEvent> syncHandler)
                    {
                        tasks.Add(Task.Run(() => syncHandler.Handle(eventData)));
                    }
                }
            }

            await Task.WhenAll(tasks);
        }
    }

    public void Publish<TEvent>(TEvent eventData) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            lock (_lock)
            {
                foreach (var handler in handlers.ToList())
                {
                    if (handler is ISyncEventHandler<TEvent> syncHandler)
                    {
                        syncHandler.Handle(eventData);
                    }
                    else if (handler is IEventHandler<TEvent> asyncHandler)
                    {
                        // Запускаем асинхронный обработчик в фоне
                        Task.Run(() => asyncHandler.HandleAsync(eventData));
                    }
                }
            }
        }
    }

    public void PublishLater<TEvent>(TEvent eventData, TimeSpan delay) where TEvent : IEvent
    {
        var executeAt = DateTime.UtcNow.Add(delay);
        _delayedEvents.Enqueue((eventData, executeAt));
    }

    private void ProcessDelayedEvents(object? state)
    {
        var now = DateTime.UtcNow;
        var eventsToProcess = new List<IEvent>();

        // Собираем события, которые нужно выполнить
        while (_delayedEvents.TryPeek(out var delayedEvent) && delayedEvent.executeAt <= now)
        {
            if (_delayedEvents.TryDequeue(out delayedEvent))
            {
                eventsToProcess.Add(delayedEvent.eventData);
            }
        }

        // Выполняем события
        foreach (var eventData in eventsToProcess)
        {
            Publish(eventData);
        }
    }

    public void Dispose()
    {
        _delayedEventTimer?.Dispose();
    }
}