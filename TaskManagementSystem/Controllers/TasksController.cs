using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Interfaces;

namespace TaskManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles ="Create")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Create")]
        public async Task<IActionResult> Create([FromForm] Entities.Task Task)
        {
            try
            {
                await _taskService.CreateTask(Task);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            
        }
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> ChangeTaskStatus(int taskId, int taskstatus)
        {
            
            try
            {
                await _taskService.ChangeTaskStatus(taskId, taskstatus);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }
    }
}
