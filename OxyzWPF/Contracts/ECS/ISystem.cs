namespace OxyzWPF.Contracts.ECS
{
    /// <summary>
    /// Базовый интерфейс для всех систем
    /// </summary>
    public interface ISystem
    {
        /// <summary>
        /// Обновляет систему
        /// </summary>
        void Update(double deltaTime);
    }
}