﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Host.Areas.Admin
{
    [Authorize(Roles = "admin")]
    public abstract class AdminController : DefaultController { }
}