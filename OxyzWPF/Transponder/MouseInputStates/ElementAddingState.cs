using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using SharpDX;

namespace OxyzWPF.Transponder.MouseInputStates;

public class ElementAddingState : IMouseInputState
{
    private readonly IMessenger _messenger;

    public string Name => "ElementAddingState";
    public Vector2 ScreenPoint { get; }
    public Vector3 ScenePoint { get; }

    public ElementAddingState(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void OnMouseClick(object sender, OxyzMouseEventArgs e)
    {
        var instructionCallargs = new InstructionCallEventArgs();
        instructionCallargs.ScenePoint = e.ScenePoint;
        _messenger.Publish(EventEnum.InstructionExecuteCall.ToString(), this, instructionCallargs);
    }

    public void OnMouseMove(object sender, OxyzMouseEventArgs e)
    {
        throw new NotImplementedException();
    }
}
