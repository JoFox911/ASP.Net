using System;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Data.Models.Account
{
    public class User: BaseModel
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
    }
}