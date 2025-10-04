namespace OxyzWPF.Contracts.Instruction;
public interface IInstructor
{
    public Dictionary<string, IInstruction> Instructions { get; }
}
