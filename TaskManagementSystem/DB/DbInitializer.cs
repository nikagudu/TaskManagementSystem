using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DB
{
    public static class DbInitializer
    {


        public static async void initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<TaskManagementDbContext>();
            context.Database.EnsureCreated();
            if (context.Tasks.Any())
            {
                return;
            }

            var tasks = new Entities.Task[]
            {
                new Entities.Task{Title= "Task1", Description="Task1 Description", DateCreated = DateTime.Now, Priority = Enums.TaskPriority.Low, Status = Enums.TasksStatus.New},
                new Entities.Task{Title= "Task2", Description="Task2 Description", DateCreated = DateTime.Now, Priority = Enums.TaskPriority.Medium, Status = Enums.TasksStatus.InProgress},
                new Entities.Task{Title= "Task3", Description="Task1 Description", DateCreated = DateTime.Now, Priority = Enums.TaskPriority.High, Status = Enums.TasksStatus.Done},
                new Entities.Task{Title= "Task4", Description="Task4 Description", DateCreated = DateTime.Now, Priority = Enums.TaskPriority.Low, Status = Enums.TasksStatus.New},
                new Entities.Task{Title= "Task5", Description="Task5 Description", DateCreated = DateTime.Now, Priority = Enums.TaskPriority.Medium, Status = Enums.TasksStatus.InProgress},
                new Entities.Task{Title= "Task6", Description="Task6 Description", DateCreated = DateTime.Now, Priority = Enums.TaskPriority.High },

            };

            foreach (var task in tasks)
            {
                context.Tasks.Add(task);
            }

            await CreateUsersAndRoles(serviceProvider);

        }

        private static async Task CreateUsersAndRoles(IServiceProvider serviceProvider)
        {
           
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Create", "Edit"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
   
            var user = new IdentityUser
            {
                UserName = "User",
                Email = "user@test.com",
            };

            var support = new IdentityUser
            {
                UserName = "Support",
                Email = "Support@test.com",
            };

            //password for both users
            string password = "Tbilisi123!";
            var _user = await UserManager.FindByNameAsync(user.UserName);

            if (_user == null)
            {
                var createUser = await UserManager.CreateAsync(user, password);
                if (createUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(user, "Create");
                }
            }

            var _support = await UserManager.FindByNameAsync(support.UserName);
            if (_support == null)
            {
                var createSupport = await UserManager.CreateAsync(support, password);
                if (createSupport.Succeeded)
                {
                    await UserManager.AddToRoleAsync(support, "Edit");
                }
            }
        }

    }
}
