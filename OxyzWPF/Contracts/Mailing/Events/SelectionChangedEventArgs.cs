namespace OxyzWPF.Contracts.Mailing.Events;

public sealed class SelectionChangedEventArgs : EventArgs
{
    private List<int> _selectionIds;

    public SelectionChangedEventArgs(List<int> selectionIds)
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
