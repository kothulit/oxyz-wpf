using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Mailing;

class Mailer : IMailer
{
    private Dictionary<EventEnum, Delegate> _eventTable = new();
    private Dictionary<EventEnum, List<object>> _publishedEvents = new();
    private Dictionary<EventEnum, List<object>> _onUpdateEvents = new();

    public void Subscribe(EventEnum eventName, Action callback)
    {
        if (_eventTable.Keys.Contains(eventName))
        {
            Action.Combine(_eventTable[eventName], callback);
        }
        else
        {
            _eventTable.Add(eventName, callback);
        }
    }

    public void Subscribe<T>(EventEnum eventName, Action<T> callback)
    {
        if (_eventTable.Keys.Contains(eventName))
        {
            Action.Combine(_eventTable[eventName], callback);
        }
        else
        {
            _eventTable.Add(eventName, callback);
        }
    }

    public void Unsubscribe(EventEnum eventName, Action method)
    {
        if (_eventTable.Keys.Contains(eventName))
        {
            Action.Remove(_eventTable[eventName], method);
        }
    }

    public void Publish(EventEnum eventName, object arg)
    {
        foreach (var eventSubscriber in _eventTable)
        {
            if (eventSubscriber.Key == eventName)
            {
                if (_publishedEvents.ContainsKey(eventName))
                {
                    _publishedEvents[eventName].Add(arg);
                }
                else
                {
                    _publishedEvents.Add(eventName, new List<object>() { arg });
                }
            }
        }
    }

    public void Update(double deltaTime)
    {
        _onUpdateEvents = new Dictionary<EventEnum, List<object>>(_publishedEvents);
        _publishedEvents.Clear();
        foreach (var eventName in _onUpdateEvents)
        {
            foreach(var eventArg in _onUpdateEvents[eventName.Key])
            {
                if (!(eventArg is null))
                {
                _eventTable[eventName.Key].DynamicInvoke(eventArg);
                }
            }
        }
    }
}
