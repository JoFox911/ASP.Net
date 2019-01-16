using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Business.Services.Base;
using TrainingApp.Data.DTO.Account;
using TrainingApp.Data.Models.Account;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    using IUserServ = IBaseService<UserListDTO, UserDetailDTO, User>;

    public class UsersController : AdminController
    {
        private readonly IUserServ userServ;

        public UsersController(
            IUserServ userServ)
        {
            this.userServ = userServ;
        }

        public async Task<IActionResult> Journal()
        {
            var ers = await userServ.GetDTO().ToListAsync();
            var users = await userServ.GetListDTO().ToListAsync();
            return View(users);
        }

    }
}