using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;

namespace OxyzWPF.Game.States;

public class GameStateMachine : IGameStateMachine
{
    private IMessenger _messenger;
    private IGameState _currentState;
    public IGameState CurrentState => _currentState;
    private Dictionary<string, IGameState> _states;

    public GameStateMachine(IMessenger messenger)
    {
        _messenger = messenger;
        _states = new Dictionary<string, IGameState>()
        {
            { "Browse", new StateBrowse(_messenger) },
            { "Edit", new StateEdit(_messenger) },
            { "Add", new StateAdd(_messenger) }
        };
        _currentState = _states["Browse"];
        _messenger.Subscribe<GameStateChangeRequestEventArgsy>(EventEnum.GameStateChangeRequest.ToString(), OnStateChangeRequest);
    }

    private void OnStateChangeRequest(object sneder, GameStateChangeRequestEventArgsy e)
    {
        ChangeState(e.NewStateName);
    }

    public string StateName => _currentState.StateName;
    public bool IsEditingEnable => _currentState.IsEditingEnable;
    public bool IsViewPanEnable => _currentState.IsEditingEnable;
    public bool IsViewZoomEnable => _currentState.IsEditingEnable;
    public bool IsViewRotateEnable => _currentState.IsEditingEnable;

    public void ChangeState(string stateName)
    {
        if (_states.TryGetValue(stateName, out var newState))
        {
            ChangeState(newState);
        }
        else
        {
            throw new ArgumentException($"State '{stateName}' not found");
        }
    }

    public void ChangeState(IGameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
        OnCangeState(newState);
    }

    public void OnCangeState(IGameState gameState)
    {
        _messenger.Publish(EventEnum.GameStateChanged.ToString(), this, new GameStateEventArgs(gameState));
    }
    public void Update(double deltaTime)
    {
        _currentState?.Update(deltaTime);
    }
}