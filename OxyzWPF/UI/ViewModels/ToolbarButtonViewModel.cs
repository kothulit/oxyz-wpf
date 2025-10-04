using System.Drawing;
using System.Windows.Input;

namespace OxyzWPF.UI.ViewModels;

public class ToolbarButtonViewModel : ViewModelBase
{
    public string Content { get; set; } = string.Empty;
    public Color BackgroundColor { get; set; } = Color.LightBlue;
    public ICommand Command { get; set; } = null;
    public bool IsEnable { get; set; } = true;
}
