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

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    using IUserServ = IModelService<User>;
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

        public IActionResult Test()
        {
            var result = ct.Query<UserDetailDTO>();
            var results = ct.UserDetailDTO.FromSql("SELECT * FROM users").ToList();
            var results2 = ct.Query<UserDetailDTO>().FromSql("SELECT * FROM users").ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserAuthDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = await userServ.GetModel()
                    .Include(u => u.Role)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDetailDTO model)
        {
            if (ModelState.IsValid)
            {
                User user = await userServ.GetModel().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User { Email = model.Email, Password = model.Password };
                    Role userRole = await roleServ.GetModel().FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    await userServ.SaveAsync(user);

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Info", "Reports");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
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