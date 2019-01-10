using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrainingApp.Data.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}