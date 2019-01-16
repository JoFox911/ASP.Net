using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data.Contexts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TrainingApp.Data.Models.Account;
using TrainingApp.Data.DTO.Account;
using TrainingApp.Business.Services.Base;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    using IUserServ = IBaseService<UserDetailDTO, User>;
    using IRoleServ = IModelService<Role>;

    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUserServ userServ;
        private readonly IRoleServ roleServ;
        private readonly TrainingAppDbContext ct;

        //private readonly TrainingAppDbContext _context;
        public AccountController(IUserServ userServ,
            IRoleServ roleServ,
            TrainingAppDbContext ct)
        {
            this.userServ = userServ;
            this.roleServ = roleServ;
            this.ct = ct;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserAuthDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await userServ.GetDTO()
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Info", "Reports", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Пароль должен генерироваться сам и отправляться на указанную почту
        [Authorize(Roles = "admin,superadmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDetailDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = await userServ.GetModel().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {                  
                    Role userRole = await roleServ.GetModel().FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null) {
                        //model.Role = userRole.Name;
                        //model.RoleId = userRole.Id;
                        await userServ.SaveAsync(model);
                    }

                    await Authenticate(model); // аутентификация

                    return RedirectToAction("Info", "Reports");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(UserDetailDTO user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}