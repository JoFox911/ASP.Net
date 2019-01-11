using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Host.Areas.Admin
{
    [Area("Admin")]
    [Authorize]
    public abstract class DefaultController : Controller { }
}