using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Game.States;

public class StateBrowse : IGameState
{
    public string StateName => "Browse";
    public bool IsEditingEnable => true;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public void Enter()
    {
    }
    public void Exit() { }
    public void Update(double deltaTime) { /* Здесь можно обновлять камеру */ }
}
