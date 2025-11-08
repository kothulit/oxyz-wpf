using OxyzWPF.Contracts.Game.States;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class TestEventArgs : EventArgs
{
    private readonly string _message;

    public TestEventArgs(string message)
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
