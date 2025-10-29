namespace OxyzWPF.Contracts.Instruction;
public interface IInstruction
{
    string Name { get; }
    void OnStart(object args);
    void Execute(object args);
    void OnEnd(object args);

}
