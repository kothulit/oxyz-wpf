using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Systems;
using System.Windows.Controls;

namespace OxyzWPF.ECS;

public class SystemsSwitcher : ISystemsSwitcher
{
    private IMessenger _messenger;
    public Dictionary<string, bool> Toggles { get; private set; }

    public SystemsSwitcher(IMessenger messenger)
    {
        _messenger = messenger;
        Toggles = new Dictionary<string, bool>()
        {
            { nameof(RenderSystem), false },
        };

        _messenger.Subscribe<SelectionChangedEventArgs>(EventEnum.SelectionChange.ToString(), OnSelectionChanged);
    }

    public void Update()
    {
        foreach (var key in Toggles.Keys)
        {
            Toggles[key] = false;
        }
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Toggles[nameof(RenderSystem)] = true;
    }
}
