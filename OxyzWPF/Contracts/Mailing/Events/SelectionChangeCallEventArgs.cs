using SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public class SelectionChangeCallEventArgs : EventArgs
{
    public List<int> SelectedElementsIds { get; set; } = new List<int>();
    public Vector2 ScreenPoint { get; set; }
}
