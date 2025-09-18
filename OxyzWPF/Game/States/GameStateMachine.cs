using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Game.States;

public class GameStateMachine
{
    private IEditorState _currentState;
    private IEditorState _defaultState = new StateNavigation();
    private Dictionary<string, IEditorState> _states;

    public GameStateMachine()
    {
        _states = new Dictionary<string, IEditorState>()
        {
            { "Default", _defaultState },
            { "Navigation", new StateNavigation() },
            { "Edit", new StateEdit() }
        };
        _currentState = _states["Default"];
    }

    public IEditorState CurrentState => _currentState;
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

    public void ChangeState(IEditorState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update(double deltaTime)
    {
        _currentState?.Update(deltaTime);
    }
}