using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Game.States;
using SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class MouseMovedEventArgs : EventArgs
{
    public MouseMovedEventArgs(Vector2 position)
    {
        Position = position;
    }

    public Vector2 Position { get; }
}
