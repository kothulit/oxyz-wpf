using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.Game.States;
using System.Windows.Input;

namespace OxyzWPF.Transponder;

public class InputTransponder : IInputTransponder
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IMailer _mailer;
    public InputTransponder(IMailer mailer, IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
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
