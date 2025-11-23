using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;

namespace OxyzWPF.Game.States;

public class ElementAddingState : BaseState, IGameState
{
    public new string StateName => typeof(ElementAddingState).ToString();
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public ElementAddingState(IMessenger messenger) : base(messenger) { }

    public void Enter() { }
    public void Exit() { }
    public void Update(double deltaTime) { }
}
