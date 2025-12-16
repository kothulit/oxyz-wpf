namespace OxyzWPF.Contracts.Instruction;
public interface IInstruction
{
    public string Name { get; }
    void OnStart(object args);
    void Execute(object args);
    void OnEnd(object args);

}
