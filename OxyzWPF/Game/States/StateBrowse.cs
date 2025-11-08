using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;

namespace OxyzWPF.Game.States;

public class StateBrowse : BaseState, IGameState
{
    public new string StateName => "Browse";
    public bool IsEditingEnable => true;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public StateBrowse(IMessenger messenger) : base (messenger) { }
    public void Enter() => _messenger.Publish(EventEnum.TestEvent.ToString(), this, new TestEventArgs($"Включено состояние {StateName}"));
    public void Exit() => _messenger.Publish(EventEnum.TestEvent.ToString(), this, new TestEventArgs($"Выключено состояние {StateName}"));
    public void Update(double deltaTime) { /* Здесь можно обновлять камеру */ }
}
