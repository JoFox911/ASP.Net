using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Host.Areas.Admin
{
    // вдальнейшем, как появятся роли можно будет уйти от общего контроллера предка и на каждый контроллер(или метод) вешать 
    //[Authorize(Roles = "Admin")] или другие роли
    [Area("Admin")]
    [Authorize]
    public abstract class AdminController : Controller { }
}