using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]      
        public async Task<IActionResult> LogIn(string username, string password)
        {
            try
            {
                var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (signInResult.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest("UserName or Password incorect");
                }
            }
            catch (Exception ex )
            {

                return BadRequest(ex);
            }            
        }
       
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Users");
        }
    }
}
