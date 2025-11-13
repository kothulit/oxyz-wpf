using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;

namespace OxyzWPF.Game.States;

public class StateEdit : BaseState, IGameState
{
    public new string StateName => "Edit";
    public bool IsEditingEnable => false;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public StateEdit(IMessenger messenger) : base(messenger) { }

    public void Enter() => _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs($"Включено состояние {StateName}"));
    public void Exit() => _messenger.Publish(EventEnum.StatusChangedEvent.ToString(), this, new StatusEventArgs($"Выключено состояние {StateName}"));
    public void Update(double deltaTime) { /* Здесь обновляем выделенные объекты */ }
}