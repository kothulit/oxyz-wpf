using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Game.States;

public class GameStateMachine
{
    private IMailer _mailer;
    private IGameState _currentState;
    private IGameState _defaultState = new StateNavigation();
    private Dictionary<string, IGameState> _states;

    public GameStateMachine(IMailer mailer)
    {
        _mailer = mailer;
        _states = new Dictionary<string, IGameState>()
        {
            { "Default", _defaultState },
            { "Navigation", new StateNavigation() },
            { "Edit", new StateEdit() }
        };
        _currentState = _states["Default"];
    }

    public IGameState CurrentState => _currentState;
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
        _mailer.Publish(EventEnum.GameStateChanged, gameState);
    }
    public void Update(double deltaTime)
    {
        _currentState?.Update(deltaTime);
    }
}