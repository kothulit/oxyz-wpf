using OxyzWPF.ECS.Systems;

namespace OxyzWPF.Contracts.ECS;

/// <summary>
/// Базовый интерфейс для всех систем
/// </summary>
public interface ISystem
{
    public string Name { get; }
    public bool IsEnable { get; set; }
    /// <summary>
    /// Обновляет систему
    /// </summary>
    void Update(double deltaTime);
}