namespace OxyzWPF.Contracts.Game;

internal interface ITransaction
{
    string Name { get; }
    void Commit();
}
