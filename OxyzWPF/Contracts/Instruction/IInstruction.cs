namespace OxyzWPF.Contracts.Instruction;
public interface IInstruction
{
    void OnStart(object args);
    void Execute(object args);
    void OnEnd();

}
