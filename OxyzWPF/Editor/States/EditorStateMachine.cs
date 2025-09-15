namespace OxyzWPF.Editor.States;

public class EditorStateMachine
{
    private IEditorState _currentState;

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

    public void HandleMouseClick(double x, double y)
    {
        _currentState?.OnMouseClick(x, y);
    }
}