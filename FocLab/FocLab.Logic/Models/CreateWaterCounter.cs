using FocLab.Logic.Resources;
using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models
{
    public class CreateWaterCounter
    {
        /// <summary>
        /// Идентифкатор дома
        /// </summary>
        public int HouseId { get; set; }

        /// <summary>
        /// Заводской номер
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(MainResources.FactoryNumberIsRequired), ErrorMessageResourceType = typeof(MainResources))]
        public string FactoryNumber { get; set; }
    }
}