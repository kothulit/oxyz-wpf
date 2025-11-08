using OxyzWPF.Contracts.Game.States;
using OxyzWPF.Contracts.Instruction;

namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class InstructionEventArgs : EventArgs
{
    private readonly IInstruction _instruction;

    public InstructionEventArgs(IInstruction instruction)
    {
        _instruction = instruction;
    }

    public IInstruction Instruction
    {
        get
        {
            return _instruction;
        }
    }
}
