using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Mailing;

class Mailer : IMailer
{
    Dictionary<string, Delegate> _eventTable = new();
    Dictionary<string, List<object>> _publishedEvents = new();

    public void Subscribe(string eventName, Action callback)
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

    public void Subscribe<T>(string eventName, Action<T> callback)
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

    public void Unsubscribe(string eventName, Action method)
    {
        if (_eventTable.Keys.Contains(eventName))
        {
            Action.Remove(_eventTable[eventName], method);
        }
    }

    public void Publish(string eventName, object arg)
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
        foreach (var eventName in _publishedEvents)
        {
            foreach(var eventArg in _publishedEvents[eventName.Key])
            {
                if (!(eventArg is null))
                {
                _eventTable[eventName.Key].DynamicInvoke(eventArg);
                }
            }
        }
        _publishedEvents.Clear();
    }
}
