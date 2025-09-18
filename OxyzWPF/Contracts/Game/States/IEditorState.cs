namespace OxyzWPF.Contracts.Game.States;
public interface IEditorState
{
    string StateName { get; }
    bool IsEditingEnable { get; }
    bool IsViewPanEnable { get; }
    bool IsViewZoomEnable { get; }
    bool IsViewRotateEnable { get; }

    void Enter();
    void Exit();
    void Update(double deltaTime);
}