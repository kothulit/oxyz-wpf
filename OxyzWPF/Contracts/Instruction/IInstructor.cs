namespace OxyzWPF.Contracts.Instruction;
public interface IInstructor
{
    public IInstruction? ActiveInstruction { get; set; }
    public Dictionary<int, IInstruction> Instructions { get; }
}
