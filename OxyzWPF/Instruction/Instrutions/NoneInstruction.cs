using OxyzWPF.Contracts.Instruction;

namespace OxyzWPF.Instruction.Instrutions;

internal class NoneInstruction : IInstruction
{
    public string Name => nameof(NoneInstruction);

    public void Execute(object _) { }

    public void OnEnd(object _) { }

    public void OnStart(object _) { }
}