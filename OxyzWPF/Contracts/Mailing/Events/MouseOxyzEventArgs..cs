using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Game.States;
using SharpDX;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class MouseOxyzEventArgs : EventArgs
{
    public MouseOxyzEventArgs(Vector2 position)
    {
        Position = position;
    }

    public Vector2 Position { get; }
}
