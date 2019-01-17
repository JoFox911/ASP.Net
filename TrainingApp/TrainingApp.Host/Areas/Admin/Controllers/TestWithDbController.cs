using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TrainingApp.Business.Services.Base;
using TrainingApp.Data.DTO.Account;
using TrainingApp.Data.Models.Account;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    using IUserServ = IBaseService<UserAuthDTO, UserDetailDTO, User>;

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