namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class InstructionEventArgs : EventArgs
{
    public int NumberOfInstruction { get; private set; }

    public InstructionEventArgs(int numberOfInstruction)
    {
        NumberOfInstruction = numberOfInstruction;
    }
}
