using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Game.States;

public class GameStateMachine
{
    private IEditorState _currentState;
    private readonly Dictionary<string, IEditorState> _states;

    public GameStateMachine()
    {
        _states = new Dictionary<string, IEditorState>()
        {
            { "Browse", new StateNavigation() },
            { "Edit", new StateEdit() }
        };
        _currentState = _states["Browse"];
    }

    public IEditorState CurrentState => _currentState;
    public string StateName => _currentState?.StateName ?? "None";
    public bool IsEditingEnable => _currentState?.IsEditingEnable ?? false;
    public bool IsViewPanEnable => _currentState?.IsEditingEnable ?? true;
    public bool IsViewZoomEnable => _currentState?.IsEditingEnable ?? true;
    public bool IsViewRotateEnable => _currentState?.IsEditingEnable ?? true;

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