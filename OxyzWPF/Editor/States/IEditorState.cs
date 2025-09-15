namespace OxyzWPF.Editor.States;
public interface IEditorState
{
    void Enter();
    void Exit();
    void Update(double deltaTime);
    void OnMouseClick(double x, double y);
}