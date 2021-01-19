using System.Collections.Generic;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;
using Montemdraco.NeuralUtils.Library.Model.Teachers;

namespace Montemdraco.NeuralUtils.Library.Interfaces.Teachers
{
    /// <summary>
    /// Интерфейс учителя нейронных сетей.
    /// </summary>
    public interface INeuralNetTeacher
    {
        /// <summary>
        /// Задает нейронную сеть для обучения.
        /// </summary>
        /// <param name="net">Экземпляр нейронной сети.</param>
        void SetNeuralNet(INeuralNet net);

        /// <summary>
        /// Добавляет один урок в очередь обучения.
        /// </summary>
        /// <param name="lessonData">Данные урока.</param>
        void AddLesson(LessonData lessonData);

        /// <summary>
        /// Добавляет коллекцию уроков в очередь обучения.
        /// </summary>
        /// <param name="collection">Коллекция уроков.</param>
        void AddLessonRange(IEnumerable<LessonData> collection);

        /// <summary>
        /// Очищает очередь обучения.
        /// </summary>
        void ClearLessons();

        /// <summary>
        /// Начинает обучение с заданным количеством эпох.
        /// </summary>
        /// <param name="epochCount">Количество эпох обучения.</param>
        void Teach(int epochCount);
    }
}