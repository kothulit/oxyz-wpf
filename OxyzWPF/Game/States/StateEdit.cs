using OxyzWPF.Contracts.Game.States;
using System.Diagnostics;


namespace OxyzWPF.Game.States;

public class StateEdit : IGameState
{
    public string StateName => "Edit";
    public bool IsEditingEnable => false;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;

    public void Enter() => Debug.WriteLine("Edit mode ON");
    public void Exit() => Debug.WriteLine("Edit mode OFF");
    public void Update(double deltaTime) { /* Здесь обновляем выделенные объекты */ }
}