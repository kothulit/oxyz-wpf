using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.Game.States;

public abstract class BaseState
{
    protected IMessenger _messenger;
    public string StateName => "Base";
    public BaseState(IMessenger messenger)
    {
        _messenger = messenger;
    }
}
