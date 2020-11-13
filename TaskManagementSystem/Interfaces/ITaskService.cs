using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskService
    {
        Task<List<Entities.Task>> GetTasks();
        Task CreateTask(Entities.Task Task);
        Task ChangeTaskStatus(int TaskId, int TaskStatus);
    }
}
