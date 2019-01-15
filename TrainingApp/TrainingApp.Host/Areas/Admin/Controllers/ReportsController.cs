using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    public class ReportsController : DefaultController
    {
        
        public IActionResult Info()
        {
            return View();
        }

        public IActionResult Test()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return Content($"ваша роль: {role}");
        }
    }
}