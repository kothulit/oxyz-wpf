using System.Windows.Input;

namespace OxyzWPF.Contracts.Transponder;

public interface IInputTransponder
{
    void OnKeyDown(object args, KeyEventArgs e);
}
