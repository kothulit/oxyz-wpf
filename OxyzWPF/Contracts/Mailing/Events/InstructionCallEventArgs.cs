using SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

internal class InstructionCallEventArgs : EventArgs
{
    public Vector3 ScenePoint { get; set; }
}
