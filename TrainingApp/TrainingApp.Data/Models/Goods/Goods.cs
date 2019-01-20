using System;
using System.Collections.Generic;
using System.Text;
using TrainingApp.Data.Base.Models;

namespace TrainingApp.Data.Models.Goods
{
    public class Goods : BaseModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Count { get; set; }
    }
}
