using System;
using System.Collections.Generic;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Data.Models.Account
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
