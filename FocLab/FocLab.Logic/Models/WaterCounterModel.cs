namespace FocLab.Logic.Models
{
    public class WaterCounterModel
    {
        /// <summary>
        /// Идентификатор счетчика
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текущие показания счетчика
        /// </summary>
        public decimal CurrentIndication { get; set; }

        /// <summary>
        /// Заводской номер
        /// </summary>
        public string FactoryNumber { get; set; }
    }
}