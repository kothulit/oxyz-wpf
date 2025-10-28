using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;
using OxyzWPF.ECS.Systems;

namespace OxyzWPF.ECS;

public class SystemsSwitcher : ISystemsSwitcher
{
    private IMailer _mailer;
    public Dictionary<string, bool> Toggles { get; private set; }

    public SystemsSwitcher(IMailer mailer)
    {
        _mailer = mailer;
        Toggles = new Dictionary<string, bool>()
        {
            { nameof(RenderSystem), false },
            { nameof(SelectionSystem), false },
        };

        _mailer.Subscribe(EventEnum.SelectionChange, OnSelectionChanged);
    }

    public void Update()
    {
        foreach (var key in Toggles.Keys)
        {
            Toggles[key] = false;
        }
    }

    private void OnSelectionChanged()
    {
        Toggles[nameof(SelectionSystem)] = true;
    }
}
