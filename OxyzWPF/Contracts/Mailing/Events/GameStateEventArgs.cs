using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class GameStateEventArgs : EventArgs
{
    public GameStateEventArgs(IGameState currentState)
    {
        CurrentState = currentState;
    }

    public IGameState CurrentState { get; }
}
