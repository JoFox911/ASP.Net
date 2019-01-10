using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    public class ReportsController : AdminController
    {
        public IActionResult Info()
        {
            return View();
        }
    }
}