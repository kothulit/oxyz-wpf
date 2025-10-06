using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Contracts.Game;

public interface IGameStateMachine
{

    public IGameState CurrentState { get; }
    void ChangeState(string stateName);
    void ChangeState(IGameState newState);
    public void OnCangeState(IGameState gameState);
    public void Update(double deltaTime);
}