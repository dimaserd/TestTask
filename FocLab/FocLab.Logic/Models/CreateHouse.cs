using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models
{
    public class CreateHouse
    {
        [MaxLength(120, ErrorMessage = "Длина адреса не должна превышать 120 символов")]
        public string Address { get; set; }
    }
}