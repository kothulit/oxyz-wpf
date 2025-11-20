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
    private KeyBindings _keyBindings;

    public InputTransponder(IMessenger messenger, IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        _messenger = messenger;
        _keyBindings = KeyBindings.Instance;
        _messenger.Subscribe<KeyEventArgs>(EventEnum.KeyPress.ToString(), OnKeyDown);
    }

    public void OnKeyDown(object? _, KeyEventArgs e)
    {
        string[] activity = ["None", "None"];
        if (e != null)
        {
            if (_keyBindings.Bindings.ContainsKey(e.Key))
            {
                activity = _keyBindings.Bindings[e.Key].Split(":");
            }
        }
        switch (activity[0])
        {
            case "Cancel":
                _messenger.Publish(EventEnum.Сancellation.ToString(), this, new EventArgs());
                break;
            case "Enter":
                _messenger.Publish(EventEnum.Сancellation.ToString(), this, new EventArgs());
                break;
            case "Command":
                if (activity.Length > 1)
                {
                    var commandNumber = 0;
                    if (Int32.TryParse(activity[1], out commandNumber))
                    {
                        _messenger.Publish(EventEnum.InstructionStart.ToString(), this, new InstructionEventArgs(commandNumber));
                    }
                }
                break;
            default:
                break;
        }
    }

    private void PublishInstruction(string instruction)
    {
        _messenger.Publish(instruction, this, new EventArgs());
    }
}
