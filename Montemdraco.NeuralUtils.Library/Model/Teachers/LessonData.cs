using System.Collections.Generic;

namespace Montemdraco.NeuralUtils.Library.Model.Teachers
{
    /// <summary>
    /// Информация о уроке.
    /// </summary>
    public class LessonData
    {
        /// <summary>
        /// Коллекция ошибок обучения.
        /// </summary>
        private readonly List<double> _accruedErrors;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LessonData"/>.
        /// </summary>
        /// <param name="input">Контейнер входных данных.</param>
        /// <param name="output">Контейнер выходных данных.</param>
        public LessonData(NeuralInputData input, NeuralOutputData output)
        {
            _accruedErrors = new List<double>();

            ExpectedInput = input;
            ExpectedOutput = output;
        }

        /// <summary>
        /// Получает входные данные нейронной сети.
        /// </summary>
        public NeuralInputData ExpectedInput { get; }
        
        /// <summary>
        /// Получает ожидаемый выход нейронной сети.
        /// </summary>
        public NeuralOutputData ExpectedOutput { get; }

        /// <summary>
        /// Получает коллекцию ошибок обучения.
        /// </summary>
        public IReadOnlyList<double> AccruedErrors => _accruedErrors;

        /// <summary>
        /// Добавляет новую ошибку обучения.
        /// </summary>
        /// <param name="error">Значение ошибки.</param>
        public void AddAccruedError(double error)
        {
            _accruedErrors.Add(error);
        }

        /// <summary>
        /// Очищает контейнер с ошибками обучеения.
        /// </summary>
        public void ClearAccruedErrors()
        {
            _accruedErrors.Clear();
        }
    }
}
