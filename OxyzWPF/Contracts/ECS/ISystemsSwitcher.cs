namespace OxyzWPF.Contracts.ECS;

public interface ISystemsSwitcher
{
    public Dictionary<string, bool> Toggles { get; }
    public void Update();
}
