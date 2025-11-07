using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class GameChangeEventArgs : EventArgs
{
    private readonly IGameState _currentState;

    public GameChangeEventArgs(IGameState currentState)
    {
        _currentState = currentState;
    }

    public IGameState CurrentState
    {
        get
        {
            return _currentState;
        }
    }
}
