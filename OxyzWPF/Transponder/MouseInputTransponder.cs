using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.Transponder.MouseInputStates;
using SharpDX;

namespace OxyzWPF.Transponder;

public class MouseInputTransponder : IMouseInputTransponder
{
    private readonly IMessenger _messenger;
    private readonly Dictionary<string, IMouseInputState> _mouseInputStates;

    public IMouseInputState State { get; set; }
    public Vector2 ScreenPoint { get; set; }
    public Vector3 ScenePoint { get; set; }

    public MouseInputTransponder(IMessenger messenger)
    {
        _messenger = messenger;
        _mouseInputStates = new Dictionary<string, IMouseInputState>()
        {
            { "ElementAddingState", new ElementAddingState(_messenger) }
        };
        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), ChandgeState);
    }

    public void ChandgeState(object _, GameStateEventArgs e)
    {
        State = _mouseInputStates["ElementAddingState"];
    }
    public void OnMouseClick(object _, OxyzMouseEventArgs e) => State.OnMouseClick(_, e);
    public void OnMouseMove(object _, OxyzMouseEventArgs e) => State.OnMouseMove(_, e);
}
