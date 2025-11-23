using SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public class InstructionCallEventArgs : EventArgs
{
    public List<int> SelectedElementsIds {  get; set; } = new List<int>();
    public Vector3 ScenePoint { get; set; }
}
