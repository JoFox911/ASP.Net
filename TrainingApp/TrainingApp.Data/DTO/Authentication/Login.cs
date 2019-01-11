using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Data.DTO.Authentication
{
    public class Login
    {
        [DisplayName("Email")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
