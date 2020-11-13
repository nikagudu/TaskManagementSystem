﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
