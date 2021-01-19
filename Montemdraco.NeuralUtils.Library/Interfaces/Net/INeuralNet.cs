using System.Collections.Generic;
using Montemdraco.NeuralUtils.Library.Model;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Net
{
    /// <summary>
    /// Интерфейс нейронной сети.
    /// </summary>
    public interface INeuralNet
    {
        /// <summary>
        /// Получает коллекцию узлов нейронной сети.
        /// </summary>
        IReadOnlyList<INeuralNode> Nodes { get; }

        /// <summary>
        /// Задает входные данные лоя нейронной сети.
        /// </summary>
        /// <param name="input">Контейнер с входными данными.</param>
        void SetInput(NeuralInputData input);

        /// <summary>
        /// Получает текущие выходные данные нейронной сети.
        /// </summary>
        /// <returns>Контейнер с выходными данными.</returns>
        NeuralOutputData GetOutput();

        /// <summary>
        /// Запускает обработку данных.
        /// </summary>
        void Run();

        /// <summary>
        /// Сохраняет состояние нейронной сети на диск.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        void SaveState(string filePath);

        /// <summary>
        /// Загружает состояние нейронной сети из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        void LoadState(string filePath);
    }
}
