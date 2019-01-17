using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Business.Services.Base;
using TrainingApp.Data.DTO.Account;
using TrainingApp.Data.Models.Account;

namespace TrainingApp.Host.Areas.Admin.Controllers
{
    using IUserServ = IBaseService<UserListDTO, UserDetailDTO, User>;
    using IRoleServ = IModelService<Role>;

    public class UsersController : AdminController
    {
        private readonly IUserServ userServ;
        private readonly IRoleServ roleServ;
        private readonly bool DisableInsteadOfDelete = true;

        public UsersController(
            IUserServ userServ,
            IRoleServ roleServ)
        {
            this.userServ = userServ;
            this.roleServ = roleServ;
        }

        public async Task<IActionResult> Journal()
        {
            var users = await userServ.GetListDTO().ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Profile(Guid? id)
        {
            UserDetailDTO model = null;

            if (id == null)
                model = Activator.CreateInstance<UserDetailDTO>();
            else
            {
                model = await userServ.GetDetailDTO().SingleOrDefaultAsync(x => x.Id == id.Value);
                if (model == null)
                {
                    return NotFound();
                }
            }

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        ////Пароль должен генерироваться сам и отправляться на указанную почту
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(UserDetailDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await userServ.GetModel().FirstOrDefaultAsync(u => u.Email == model.Email);
        //        if (user == null)
        //        {
        //            Role userRole = await roleServ.GetModel().FirstOrDefaultAsync(r => r.Name == "user");
        //            if (userRole != null)
        //            {
        //                //model.Role = userRole.Name;
        //                //model.RoleId = userRole.Id;
        //                await userServ.SaveAsync(model);
        //            }

        //            await Authenticate(model); // аутентификация

        //            return RedirectToAction("Info", "Reports");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}

        public virtual async Task<IActionResult> Edit(Guid? id)
        {
            UserDetailDTO model = null;

            var roles = await roleServ.GetModel().ToListAsync();
            var listItems = new List<SelectListItem>();
            foreach (var role in roles)
            {
                listItems.Add(new SelectListItem { Selected = false, Text = role.Name, Value = role.Id.ToString() });
            }
            var rolesSelectList = new SelectList(listItems, "Value", "Text");

            ViewBag.Roles = rolesSelectList;

            if (id == null)
                model = Activator.CreateInstance<UserDetailDTO>();
            else
            {
                model = await userServ.GetDetailDTO().SingleOrDefaultAsync(x => x.Id == id.Value);
                if (model == null)
                {
                    return NotFound();
                }
            }
            
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserDetailDTO model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (id == Guid.Empty)
            {
                var user = await userServ.GetModel().FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    ModelState.AddModelError("Email", "Користувач з таким Email вже існує");
                }
            }            

            if (ModelState.IsValid)
            {
                model.Password = "1";
                try
                {
                    await userServ.SaveAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await userServ.GetListDTO().SingleOrDefaultAsync(x => x.Id == id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Journal");
            }
            return View(model);
        }

        [HttpPost]
        public virtual async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                User entity;
                if (DisableInsteadOfDelete)
                    entity = userServ.Disable(id);
                else
                    entity = userServ.Remove(id);
                
                return await Task.FromResult(Json(new { success = true }));
            }
            catch (Exception e)
            {
                return await Task.FromResult(Json(new { success = false, ErrorMessage = "Помилка видалення. " + (e.InnerException ?? e).Message }));
            }
        }

    }
}