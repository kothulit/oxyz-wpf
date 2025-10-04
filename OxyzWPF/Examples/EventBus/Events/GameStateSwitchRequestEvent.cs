namespace OxyzWPF.EventBus.Events;

class GameStateSwitchRequestEvent : EventBase
{
    public override string EventType => nameof(GameStateSwitchRequestEvent);
    public string RequestedState { get; }
    public GameStateSwitchRequestEvent(string requestedState)
    {
        RequestedState = requestedState;
    }
}
