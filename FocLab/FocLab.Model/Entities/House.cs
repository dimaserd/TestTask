using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities
{
    public class House
    {
        /// <summary>
        /// Идентифкатор дома
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Адрес дома
        /// </summary>
        [MaxLength(120)]
        public string Address { get; set; }

        /// <summary>
        /// Идентификатор счетчика
        /// </summary>
        public int? WaterCounterId { get; set; }

        /// <summary>
        /// Ссылка на счетчик воды
        /// </summary>
        [ForeignKey(nameof(WaterCounterId))]
        public WaterCounter WaterCounter { get; set; }
    }
}