namespace OxyzWPF.EventBus.Events;

class GameStateChangedEvent : EventBase
{
    public override string EventType => nameof(GameStateChangedEvent);

    public string NewState { get; }
    public string PreviousState { get; }

    public GameStateChangedEvent(string newState, string previousState)
    {
        PreviousState = previousState;
        NewState = newState;
    }
}
