using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;

        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _taskService.GetTasks();
                return View(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            
        }
       
    }
}
