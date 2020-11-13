using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.DB;
using TaskManagementSystem.Interfaces;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagementDbContext _context;

        public TaskService(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Task>> GetTasks()
        {
            var result = await _context.Tasks.ToListAsync();
            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                throw new Exception();
            }
        }

        public virtual async Task CreateTask(Entities.Task Task)
        {
            Task.DateCreated = DateTime.Now;
             await _context.Tasks.AddAsync(Task);           
            _context.SaveChanges();
        }

        public async Task ChangeTaskStatus(int TaskId, int TaskStatus)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == TaskId);            
            if (task != null)
            {
                task.Status = (Enums.TasksStatus?)TaskStatus;
                _context.Tasks.Update(task);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
