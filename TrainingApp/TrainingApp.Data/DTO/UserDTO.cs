using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Data.DTO
{
    class UserListDTO
    {
        public Guid Id { get; set; }

        [DisplayName("Повне ім'я")]
        public string FullName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Роль")]
        public string Role { get; set; }
    }

    class UserDetailDTO
    {
        public Guid Id { get; set; }

        [DisplayName("Прізвище")]
        [Required(ErrorMessage = "Прізвище є обов'язковим полем")]
        public string LastName { get; set; }

        [DisplayName("По батькові")]
        [Required(ErrorMessage = "По батькові є обов'язковим полем")]
        public string SurnameName { get; set; }

        [DisplayName("Ім'я")]
        [Required(ErrorMessage = "Ім'я є обов'язковим полем")]
        public string FirstName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email є обов'язковим полем")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пароль є обов'язковим полем")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Роль є обов'язковим полем")]
        public Guid? RoleId { get; set; }
        [DisplayName("Роль")]
        public string Role { get; set; }
    }
}
