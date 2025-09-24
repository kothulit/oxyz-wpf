using OxyzWPF.Contracts.ECS;
using OxyzWPF.UI.ViewModels;

namespace OxyzWPF.Contracts.Editor;
public interface IInstructor
{
    public Dictionary<string, IInstruction> Instructions { get; }
}
