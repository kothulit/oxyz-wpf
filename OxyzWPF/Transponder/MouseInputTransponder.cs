using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using OxyzWPF.Game.States;
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
            { GameStateEnum.EntityAdding.ToString(), new ElementAddingMouseInputState(_messenger) },
            { GameStateEnum.Browsing.ToString(), new BrowsingMouseInputState(_messenger) }
        };
        State = _mouseInputStates[GameStateEnum.Browsing.ToString()];

        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), ChandgeState);
        _messenger.Subscribe<OxyzMouseEventArgs>(EventEnum.MouseDown.ToString(), OnMouseClick);
        _messenger.Subscribe<GameStateEventArgs>(EventEnum.GameStateChanged.ToString(), OnStateChanged);
    }

    public void ChandgeState(object _, GameStateEventArgs e)
    {
        State = _mouseInputStates[GameStateEnum.EntityAdding.ToString()];
    }
    public void OnMouseClick(object _, OxyzMouseEventArgs e) => State.OnMouseClick(_, e);
    public void OnMouseMove(object _, OxyzMouseEventArgs e) => State.OnMouseMove(_, e);

    private void OnStateChanged(object _, GameStateEventArgs e)
    {
        State = _mouseInputStates[e.CurrentState.StateName];
    }
}
