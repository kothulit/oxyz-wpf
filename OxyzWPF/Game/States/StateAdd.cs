using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;

namespace OxyzWPF.Game.States;

public class StateAdd : BaseState, IGameState
{
    public new string StateName => "Add";
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public StateAdd(IMessenger messenger) : base(messenger) { }

    public void Enter() { }
    public void Exit() { }
    public void Update(double deltaTime) { }
}
