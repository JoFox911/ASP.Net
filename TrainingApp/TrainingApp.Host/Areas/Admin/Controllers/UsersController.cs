using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        public IActionResult UsersJournal()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content($"ваша роль: {role}");
        }
    }
}