using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class GameStateChangeRequestEventArgsy : EventArgs
{
    public GameStateChangeRequestEventArgsy(string newStateName)
    {
        NewStateName = newStateName;
    }

    public string NewStateName { get; }

}
