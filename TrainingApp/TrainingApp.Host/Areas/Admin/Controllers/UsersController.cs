using System;
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual async Task<IActionResult> Edit(Guid id, TDetailDTO model)
        //{
        //    if (id != model.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            BeforePostEdit?.Invoke(model);
        //            await Service.SaveAsync(model, TransactedDBOperations);
        //            OnPostEdit?.Invoke(model);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (await Service.GetListDTO().SingleOrDefaultAsync(x => x.Id == id) == null)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return this.RedirectBack(nameof(Details), new { model.Id });
        //    }
        //    return View(model);
        //}

    }
}