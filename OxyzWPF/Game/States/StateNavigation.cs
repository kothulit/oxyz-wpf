using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Messenger;

namespace OxyzWPF.Game.States;

public class StateNavigation : IGameState
{
    public string StateName => "Browse";
    public bool IsEditingEnable => true;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public void Enter()
    {
        Messenger<string>.Broadcast(MessengerEvents.EditorStateChanged, "Browse");
    }
    public void Exit() { }
    public void Update(double deltaTime) { /* Здесь можно обновлять камеру */ }
}