using System.Collections.Generic;
using System.Linq;

namespace Montemdraco.NeuralUtils.Library.Model
{
    /// <summary>
    /// Контейнер входных данных для нейронной сети.
    /// </summary>
    public class NeuralInputData
    {
        /// <summary>
        /// Контейнер с входными данными.
        /// </summary>
        private Dictionary<string, double> _internalContainer;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NeuralInputData"/>.
        /// </summary>
        public NeuralInputData()
        {
            _internalContainer = new Dictionary<string, double>();
        }

        /// <summary>
        /// Получает коллекцию входных данных нейронной сети.
        /// </summary>
        public IReadOnlyDictionary<string, double> InputContainer => _internalContainer;

        /// <summary>
        /// Добавляет или обновляет входные данные.
        /// </summary>
        /// <param name="name">Название ассоциированного входа.</param>
        /// <param name="data">Входные данные.</param>
        public void AddOrUpdate(string name, double data)
        {
            name = name.ToLowerInvariant();
            if (_internalContainer.ContainsKey(name))
            {
                _internalContainer[name] = data;
            }
            else
            {
                _internalContainer.Add(name, data);
            }
        }

        /// <summary>
        /// Добавляет входные данные.
        /// Если данные, ассоциированные с этим входом уже заданы - ничего не произойдет.
        /// </summary>
        /// <param name="name">Название ассоциированного входа.</param>
        /// <param name="data">Входные данные.</param>
        public void Add(string name, double data)
        {
            name = name.ToLowerInvariant();
            if (!_internalContainer.ContainsKey(name))
            {
                AddOrUpdate(name, data);
            }
        }

        /// <summary>
        /// Добавляет входные данные в коллекцию.
        /// </summary>
        /// <param name="data">Входные данные.</param>
        public void Add(double data)
        {
            int tempValue;
            var numericKeys = _internalContainer
                .Keys
                .Where(e => int.TryParse(e, out tempValue))
                .Select(e => int.Parse(e))
                .ToList();

            var maxNumberKey = numericKeys.Count > 0 ? numericKeys.Max() : 0;

            AddOrUpdate((maxNumberKey + 1).ToString(), data);
        }
    }
}
