using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using System.Windows.Input;

namespace OxyzWPF.Transponder;

public class InputTransponder : IInputTransponder
{
    private readonly IGameStateMachine _gameStateMachine;
    private readonly IMessenger _messenger;
    public InputTransponder(IMessenger messenger, IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        _messenger = messenger;

        _messenger.Subscribe<KeyEventArgs>(EventEnum.KeyPress.ToString(), OnKeyDown);
    }

    public void OnKeyDown(object? _, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                _messenger.Publish(EventEnum.Сancellation.ToString(), this, new EventArgs());
                break;
            default:
                break;
        }
    }

}
