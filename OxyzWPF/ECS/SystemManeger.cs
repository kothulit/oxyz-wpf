using OxyzWPF.Contracts.ECS;
using OxyzWPF.Contracts.Mailing;
using System.Collections.Generic;

namespace OxyzWPF.ECS;

public class SystemManeger
{
    private readonly IMessenger _messenger;
    private Dictionary<string, bool> _permissions = new Dictionary<string, bool>();

    private List<ISystem> _systems = new List<ISystem>();
    public List<ISystem> Systems
    {
        get => _systems;
        set
        {
            _permissions.Clear();
            _systems = value;
            foreach (var system in _systems)
            {
                _permissions[system.Name] = true;
            }
        }
    }

    public SystemManeger(IMessenger messenger, List<ISystem> systems)
    {
        _messenger = messenger;
        Systems = systems;
    }

    public void Update()
    {
        if (Systems.Count == _permissions.Count)
        {
            foreach (var system in Systems)
            {
                if (_permissions[system.Name])
                {
                    system.IsEnable = _permissions[system.Name];
                }
                else throw new Exception("Не найдено разрешение для системы");
            }
        }
        else throw new Exception("Списки систем и разрешений для систем имеют разный размер");
    }
}
