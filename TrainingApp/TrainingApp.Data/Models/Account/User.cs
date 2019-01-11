using System;

namespace TrainingApp.Data.Models.Account
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SurnameName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
    }
}