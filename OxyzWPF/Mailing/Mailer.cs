using Microsoft.Extensions.DependencyInjection;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Mailing;

class Mailer : IMailer
{
    Dictionary<string, Action> _eventSubscribers = new();
    List<string> _publishedEvents = new();

    public void Subscribe(string eventName, Action method)
    {
        if (_eventSubscribers.Keys.Contains(eventName))
        {
            _eventSubscribers[eventName] += method;
        }
        else
        {
            _eventSubscribers.Add(eventName, method);
        }
    }

    public void Unsubscribe(string eventName, Action method)
    {
        if (_eventSubscribers.Keys.Contains(eventName))
        {
            _eventSubscribers[eventName] -= method;
        }
    }

    public void Publish(string eventName)
    {
        foreach (var eventSubscriber in _eventSubscribers)
        {
            if (eventSubscriber.Key == eventName)
            {
                _publishedEvents.Add(eventName);
            }
        }
    }

    public void Update(double deltaTime)
    {
        foreach (var eventName in _publishedEvents)
        {
            _eventSubscribers[eventName]?.Invoke();
        }
        _publishedEvents.Clear();
    }
}
