namespace OxyzWPF.Contracts.Editor;

internal interface ITransaction
{
    string Name { get; }
    void Commit();
}
