using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Mailing;

class Messenger : IMessenger
{
    private Dictionary<string, Delegate> _eventTable = new();
    private Dictionary<string, List<(object, EventArgs)>> _publishedEvents = new();
    private Dictionary<string, List<(object, EventArgs)>> _onUpdateEvents = new();

    public void Subscribe<TEventArgs>(string eventName, EventHandler<TEventArgs> callback) where TEventArgs : EventArgs
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

    public void Unsubscribe(string eventName, EventHandler callback)
    {
        if (_eventTable.Keys.Contains(eventName))
        {
            EventHandler.Remove(_eventTable[eventName], callback);
        }
    }

    public void Publish(string eventName, object sender, EventArgs e)
    {
        foreach (var eventSubscriber in _eventTable)
        {
            if (eventSubscriber.Key == eventName)
            {
                if (_publishedEvents.ContainsKey(eventName))
                {
                    _publishedEvents[eventName].Add((sender, e));
                }
                else
                {
                    _publishedEvents.Add(eventName, new List<(object, EventArgs)>() { (sender, e) });
                }
            }
        }
    }

    public void Update(double deltaTime)
    {
        _onUpdateEvents = new Dictionary<string, List<(object, EventArgs)>>(_publishedEvents);
        _publishedEvents.Clear();
        foreach (var eventName in _onUpdateEvents)
        {
            foreach((object, EventArgs) eventArgs in _onUpdateEvents[eventName.Key])
            {
                if (eventArgs.Item1 != null && eventArgs.Item2 != null)
                {
                _eventTable[eventName.Key].DynamicInvoke(eventArgs.Item1, eventArgs.Item2);
                }
            }
        }
    }
}
