using FocLab.Logic.Resources;
using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models
{
    public class CreateHouse
    {
        [Required(ErrorMessageResourceName = nameof(MainResources.CreateHouseAddressRequiredErrorMessage), ErrorMessageResourceType = typeof(MainResources))]
        [MaxLength(120, ErrorMessageResourceName = nameof(MainResources.CreateHouseAddressMaxLengthValidationErrorMessage), ErrorMessageResourceType = typeof(MainResources))]
        public string Address { get; set; }
    }
}