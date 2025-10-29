using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Transponder;
using System.Windows.Input;

namespace OxyzWPF.Transponder;

public class InputTransponder : IInputTransponder
{
    IMailer _mailer;
    public InputTransponder(IMailer mailer)
    {
        _mailer = mailer;
    }
    public void OnKeyDown(object args)
    {
        var keyEventArgs = args as KeyEventArgs;
        switch (keyEventArgs.Key)
        {
            case Key.Escape:
                _mailer.Publish(EventEnum.InstructionCanseled, "Escape");
                break;
            default:
                break;
        }
    }
}
