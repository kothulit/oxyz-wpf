using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.ECS;

public class SupportWorld : World
{
    public SupportWorld(IMessenger messenger) : base(messenger) { }
}
