using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Game.States;

public class StateBrowse : BaseState, IGameState
{
    public string StateName => "Browse";
    public bool IsEditingEnable => true;
    public bool IsViewPanEnable => true;
    public bool IsViewZoomEnable => true;
    public bool IsViewRotateEnable => true;
    public StateBrowse(IMailer mailer) : base (mailer) { }
    public void Enter() => _mailer.Publish(EventEnum.TestEvent, $"Включено состояние {StateName}");
    public void Exit() => _mailer.Publish(EventEnum.TestEvent, $"Выключено состояние {StateName}");
    public void Update(double deltaTime) { /* Здесь можно обновлять камеру */ }
}
