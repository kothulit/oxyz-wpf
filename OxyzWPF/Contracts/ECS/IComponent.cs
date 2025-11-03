namespace OxyzWPF.Contracts.ECS;

/// <summary>
/// Базовый интерфейс для всех компонентов
/// </summary>
public interface IComponent
{
    string Name { get; }
    // Компоненты - это просто контейнеры данных
    // Логика обрабатывается в системах
}