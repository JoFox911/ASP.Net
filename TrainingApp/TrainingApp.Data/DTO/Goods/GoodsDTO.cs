using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TrainingApp.Data.DTO.Base;

namespace TrainingApp.Data.DTO.Goods
{
    public class GoodsListDTO: BaseDTO
    {
       [DisplayName("Назва")]
        public string Name { get; set; }

        [DisplayName("Ціна")]
        public double Price { get; set; }

        [DisplayName("Кількість")]
        public double Count { get; set; }
    }

    public class GoodsDetailDTO : BaseDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Count { get; set; }
    }
}
