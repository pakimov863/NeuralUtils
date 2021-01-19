namespace Montemdraco.NeuralUtils.Library.Interfaces
{
    /// <summary>
    /// Интерфейс именованного объекта.
    /// </summary>
    public interface INamedObject
    {
        /// <summary>
        /// Получает уникальное название объекта.
        /// </summary>
        string Name { get; }
    }
}
