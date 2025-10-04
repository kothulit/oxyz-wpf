using OxyzWPF.Contracts.Game.States;
using System.Diagnostics;


namespace OxyzWPF.Game.States;

public class StateAdd : IGameState
{
    public string StateName => "Add";
    public bool IsEditingEnable => false;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    
    public void Enter() => Debug.WriteLine("Add mode ON");
    public void Exit() => Debug.WriteLine("Add mode OFF");
    public void Update(double deltaTime) { /* Здесь обновляем выделенные объекты */ }
}