using OxyzWPF.Contracts.Mailing.Events;
using SharpDX;

namespace OxyzWPF.Contracts.Transponder;

public interface IMouseInputState
{
    string Name { get; }
    public Vector2 ScreenPoint { get; }
    public Vector3 ScenePoint { get; }
    void OnMouseClick(object sender, OxyzMouseEventArgs e);
    void OnMouseMove(object sender, OxyzMouseEventArgs e);
}
