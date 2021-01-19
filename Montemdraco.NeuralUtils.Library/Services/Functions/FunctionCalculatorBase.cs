using System;
using System.Collections.Generic;
using System.Linq;
using Montemdraco.NeuralUtils.Library.Interfaces;

namespace Montemdraco.NeuralUtils.Library.Services.Functions
{
    /// <summary>
    /// Базовый класс калькулятора функций.
    /// </summary>
    /// <typeparam name="T">Тип функций.</typeparam>
    public abstract class FunctionCalculatorBase<T> where T : INamedObject
    {
        /// <summary>
        /// Коллекция функций активации.
        /// </summary>
        private IEnumerable<T> _functionRepo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FunctionCalculatorBase{T}"/>.
        /// </summary>
        /// <param name="funcs">Коллекция функций рассчета ошибки.</param>
        protected FunctionCalculatorBase(IEnumerable<T> funcs)
        {
            _functionRepo = funcs;
        }

        /// <summary>
        /// Получает функцию по имени.
        /// </summary>
        /// <param name="functionName">Имя функции.</param>
        /// <returns>Экземпляр функции.</returns>
        protected T GetFunctionByName(string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException("Function name not set.", nameof(functionName));
            }

            if (_functionRepo == null)
            {
                throw new Exception("Function container not exists.");
            }

            var namedFunc = _functionRepo.FirstOrDefault(e => e.Name.Equals(functionName, StringComparison.InvariantCultureIgnoreCase));
            if (namedFunc == null)
            {
                throw new ArgumentException("No function found.", nameof(namedFunc));
            }

            return namedFunc;
        }
    }
}
