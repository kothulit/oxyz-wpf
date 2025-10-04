namespace OxyzWPF.Contracts.Instruction;
public interface IInstruction
{
    void OnStart();
    void Execute(object args);
    void OnEnd();

}
