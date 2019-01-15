using Microsoft.AspNetCore.Authorization;

namespace TrainingApp.Host.Areas.Admin
{
    [Authorize(Roles = "admin,superadmin")]
    public abstract class AdminController : DefaultController { }
}