using OxyzWPF.Contracts.Mailing.Events;
using SharpDX;

namespace OxyzWPF.Contracts.Transponder;

public interface IMouseInputTransponder
{
    public IMouseInputState State { get; set; }
    public Vector2 ScreenPoint { get; set; }
    public Vector3 ScenePoint { get; set; }

    public void OnStateChanged(object _, GameStateEventArgs e);
    public void OnMouseClick(object _, OxyzMouseEventArgs e);
    public void OnMouseMove(object _, OxyzMouseEventArgs e);
}
