using Asp.Net.Project.Yad2.Models;
using Asp.Net.Project.Yad2.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService UserService;
        public UserController(IUserService userService, IProductService productService)
        {
            UserService = userService;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserModel user)
        {
            if (!ModelState.IsValid)
                return View(user);
            if (UserService.IsExist(user.UserName))
            {
                TempData["User Exist"] = "correct";
                ViewBag.userExist = "User Exist";
                return View(user);
            }
            else
            {
                TempData["User Exist"] = "incorrect";
                UserService.Add(user);
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Login(SignIn user) //Checks if user exists, if not creates cookies
        {
            if (user == null)
            {
                TempData["LogInError"] = "Incorrect";
                return RedirectToAction("Index", "Home");
            }
            else if (UserService.IsUserNameAndPasswordExist(user.Username, user.SignInPassword))
            {
                TempData["LogInError"] = "Correct";
                HttpContext.Response.Cookies.Append("AspProjectCookie", $"{user.Username},{user.SignInPassword}", new CookieOptions() { Expires = (DateTime.Now).AddDays(3) });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LogInError"] = "Incorrect";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult UpdateUser()
        {
            UserModel user = HttpContext.Items["CookieKey"] as UserModel;
            if (user == null) return BadRequest();
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateUser(UserModel UpdatedUser)
        {
                UserService.Update(UpdatedUser);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignOut()
        {
            HttpContext.Response.Cookies.Delete("AspProjectCookie");
            return RedirectToAction("Index", "Home");
        }

    }
}
