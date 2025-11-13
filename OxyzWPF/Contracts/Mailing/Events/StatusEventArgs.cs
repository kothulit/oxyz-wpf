using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class StatusEventArgs : EventArgs
{
    private readonly string _message;

    public StatusEventArgs(string message)
    {
        _message = message;
    }

    public string Message
    {
        get
        {
            return _message;
        }
    }
}
