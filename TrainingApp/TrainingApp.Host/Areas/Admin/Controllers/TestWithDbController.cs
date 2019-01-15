using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrainingApp.Business.Services.Base;
using TrainingApp.Data.DTO.Account;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    using IUserServ = IDTOService<UserDetailDTO>;

    [Area("Admin")]
    public class TestWithDbController : Controller
    {
        private readonly IUserServ userServ;

        public TestWithDbController(
            IUserServ userServ)
        {
            this.userServ = userServ;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userServ.GetDTO().ToListAsync();
            return View(users);
        }
    }
}