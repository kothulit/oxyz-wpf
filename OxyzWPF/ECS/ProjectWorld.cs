using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;

namespace OxyzWPF.ECS
{
    public class ProjectWorld : World
    {
        public ProjectWorld(IMessenger messenger) : base(messenger) { }
    }
}
