namespace OxyzWPF.Contracts.Game;

public interface ISelection
{
    public List<int> SelectionIds { get; }

    public void Add(int id);

    public void Remove(int id);

    public void Clear();
}
