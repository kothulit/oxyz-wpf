using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Mailing;
using System.Diagnostics;


namespace OxyzWPF.Game.States;

public class StateAdd : BaseState, IGameState
{
    public string StateName => "Add";
    public bool IsEditingEnable => false;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;

    public StateAdd(IMailer mailer) : base(mailer) { }

    public void Enter() => _mailer.Publish(EventEnum.TestEvent, $"Включено состояние {StateName}");
    public void Exit() => _mailer.Publish(EventEnum.TestEvent, $"Выключено состояние {StateName}");
    public void Update(double deltaTime) { /* Здесь обновляем выделенные объекты */ }
}