using SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class OxyzMouseEventArgs : EventArgs
{
    public OxyzMouseEventArgs(Vector2 screeenPoint)
    {
        ScreenPoint = screeenPoint;
    }
    public OxyzMouseEventArgs(Vector2 screeenPoint, Vector3 scenePoint)
    {
        ScreenPoint = screeenPoint;
        ScenePoint = scenePoint;
    }
    public OxyzMouseEventArgs(Vector2 screeenPoint, Vector3 scenePoint, List<int> hitEntityesIds)
    {
        ScreenPoint = screeenPoint;
        ScenePoint = scenePoint;
        HitEntityesIds = hitEntityesIds;
    }

    public Vector2 ScreenPoint { get; set; }
    public Vector3 ScenePoint { get; set; }
    public List<int> HitEntityesIds { get; set; }
}
