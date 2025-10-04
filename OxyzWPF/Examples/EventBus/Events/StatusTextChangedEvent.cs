namespace OxyzWPF.EventBus.Events;
class StatusTextChangedEvent : EventBase
{
    public override string EventType => nameof(StatusTextChangedEvent);
    public string NewStatusText { get; }
    public StatusTextChangedEvent(string newStatusText)
    {
        NewStatusText = newStatusText;
    }
}
