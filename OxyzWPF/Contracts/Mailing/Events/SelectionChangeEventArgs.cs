namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class SelectionChangeEventArgs : EventArgs
{
    private List<int> _selectionIds;

    public SelectionChangeEventArgs(List<int> selectionIds)
    {
        _selectionIds = selectionIds;
    }

    public List<int> SelectionIds
    {
        get
        {
            return _selectionIds;
        }
    }
}
