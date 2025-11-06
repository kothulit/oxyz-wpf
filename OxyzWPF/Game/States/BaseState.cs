using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Game.States;

public class BaseState
{
    protected IMailer _mailer;
    public string StateName => "Add";
    public BaseState(IMailer mailer)
    {
        _mailer = mailer;
    }
}
