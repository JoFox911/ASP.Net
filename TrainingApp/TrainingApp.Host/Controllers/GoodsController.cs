using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Business.Services.Base;
using TrainingApp.Data.Models.Goods;
using TrainingApp.Data.DTO.Goods;
using Microsoft.AspNetCore.Http;
using System.IO;
using ExcelDataReader;

namespace TrainingApp.Host.Controllers
{
    using IGoodsServ = IBaseService<GoodsListDTO, GoodsDetailDTO, Goods>;

    public class GoodsController : Controller
    {
        private readonly IGoodsServ goodsServ;

        public GoodsController(
            IGoodsServ goodsServ)
        {
            this.goodsServ = goodsServ;
        }

        public async Task<IActionResult> Index()
        {
            var goods = await goodsServ.GetListDTO().ToListAsync();
            return View(goods);
        }

        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
            ParseFile(file);
            return RedirectToAction("Index");
        }

        private void ParseFile(IFormFile file)
        {
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        "price");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                IExcelDataReader reader;
                reader = ExcelReaderFactory.CreateReader(stream);
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };

                var dataSet = reader.AsDataSet(conf);
                var dataTable = dataSet.Tables[0];

                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (var j = 0; j < dataTable.Columns.Count; j++)
                    {
                        var data = dataTable.Rows[i][j];
                    }
                }
            }
        }

        
    }
}
