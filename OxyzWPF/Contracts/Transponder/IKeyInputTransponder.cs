using System.Windows.Input;

namespace OxyzWPF.Contracts.Transponder;

public interface IKeyInputTransponder
{
    void OnKeyDown(object args, KeyEventArgs e);
}
