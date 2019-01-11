using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Business.Services.Users;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        
        private readonly IUsersService usersService;

        public UsersController(
            IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult UsersJournal()
        {
            //var list = await usersService.GetUsersList();

            //if (list == null)
            //{
            //    return NotFound();
            //}

            //return View(list);
            return View();
        }
    }
}