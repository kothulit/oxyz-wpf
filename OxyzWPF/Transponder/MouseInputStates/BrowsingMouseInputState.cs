using OxyzWPF.Contracts.Game;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.Contracts.Mailing.Events;
using OxyzWPF.Contracts.Transponder;
using SharpDX;

namespace OxyzWPF.Transponder.MouseInputStates;

public class BrowsingMouseInputState : IMouseInputState
{
    private readonly IMessenger _messenger;

    public string Name => GameStateEnum.Browsing.ToString();
    public Vector2 ScreenPoint { get; }
    public Vector3 ScenePoint { get; }
    public int SelectedEntityId { get; }

    public BrowsingMouseInputState(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void OnMouseClick(object sender, OxyzMouseEventArgs e)
    {
        var selectionCallargs = new SelectionChangeCallEventArgs();
        selectionCallargs.SelectedElementsIds = e.HitEntityesIds;
        _messenger.Publish(EventEnum.SelectionChangeCall.ToString(), this, selectionCallargs);
    }

    public void OnMouseMove(object sender, OxyzMouseEventArgs e)
    {
        throw new NotImplementedException();
    }
}
