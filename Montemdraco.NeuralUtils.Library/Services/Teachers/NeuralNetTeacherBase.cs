using System.Collections.Generic;
using Montemdraco.NeuralUtils.Library.Interfaces.Functions;
using Montemdraco.NeuralUtils.Library.Interfaces.Net;
using Montemdraco.NeuralUtils.Library.Interfaces.Teachers;
using Montemdraco.NeuralUtils.Library.Model.Teachers;

namespace Montemdraco.NeuralUtils.Library.Services.Teachers
{
    /// <summary>
    /// Базовый класс для учителя нейронной сети.
    /// </summary>
    public abstract class NeuralNetTeacherBase : INeuralNetTeacher
    {
        /// <summary>
        /// Функция расчета ошибки.
        /// </summary>
        protected IErrorFunction _errorFunction;

        /// <summary>
        /// Обучаемая нейронная сеть.
        /// </summary>
        protected INeuralNet _neuralNet;

        /// <summary>
        /// Коллекция уроков для обучения.
        /// </summary>
        protected IList<LessonData> _lessonContainer;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NeuralNetTeacherBase"/>.
        /// </summary>
        /// <param name="errorFunction">Используемая функция подсчета ошибки.</param>
        protected NeuralNetTeacherBase(IErrorFunction errorFunction)
        {
            _errorFunction = errorFunction;

            _lessonContainer = new List<LessonData>();
        }

        /// <inheritdoc />
        public void SetNeuralNet(INeuralNet net)
        {
            _neuralNet = net;

            foreach (var lesson in _lessonContainer)
            {
                lesson.ClearAccruedErrors();
            }
        }

        /// <inheritdoc />
        public void AddLesson(LessonData lessonData)
        {
            _lessonContainer.Add(lessonData);
        }

        /// <inheritdoc />
        public void AddLessonRange(IEnumerable<LessonData> collection)
        {
            foreach (var lesson in collection)
            {
                _lessonContainer.Add(lesson);
            }
        }

        /// <inheritdoc />
        public void ClearLessons()
        {
            _lessonContainer.Clear();
        }

        /// <inheritdoc />
        public void Teach(int epochCount)
        {
            for (var i = 0; i < epochCount; ++i)
            {
                foreach (var lessonData in _lessonContainer)
                {
                    ProcessLesson(lessonData);
                }
            }
        }

        /// <summary>
        /// Применяет один обучающий урок на нейронной сети.
        /// </summary>
        /// <param name="lesson">Данные урока.</param>
        protected abstract void ProcessLesson(LessonData lesson);
    }
}
