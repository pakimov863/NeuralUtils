namespace Montemdraco.NeuralUtils.Library.Model.Teachers.BackPropagation
{
    /// <summary>
    /// Информация о изменении весов синапсов.
    /// </summary>
    public class SynapseCorrectionInfo
    {
        /// <summary>
        /// Получает или задает градиент синапса.
        /// </summary>
        public double Gradient { get; set; }

        /// <summary>
        /// Получает или задает разницу весов.
        /// </summary>
        public double DeltaWeight { get; set; }

        /// <summary>
        /// Получает или задает новый вес связи.
        /// </summary>
        public double NewLinkWeight { get; set; }
    }
}