using OxyzWPF.Contracts.Mailing;
using System.Windows.Input;

namespace OxyzWPF.Transponder;

public sealed class KeyBindings
{
    public Dictionary<Key, string> Bindings { get; private set; }

    private static KeyBindings? _instance = null;

    public static KeyBindings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new KeyBindings();
                _instance.Bindings = GetBindings();
            }
            return _instance;
        }
    }

    private KeyBindings()
    {
        
    }

    private static Dictionary<Key, string> GetBindings()
    {
        return new Dictionary<Key, string>()
        {
            { Key.Escape, "Cancel" },
            { Key.Space , "Enter" },
            { Key.D1, "Command:1" },
            { Key.D2, "Command:2" },
            { Key.D3, "Command:3" },
        };
    }
}
