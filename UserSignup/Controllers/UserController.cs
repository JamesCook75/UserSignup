using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSignup.Models;

namespace UserSignup.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.users = UserData.GetAll();
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(User user, string verify)
        {
            bool notAlpha = (user.Username.Any(x => !char.IsLetter(x)));

            if (user.Username != null && !notAlpha && user.Username.Length >= 5 && user.Username.Length <= 15)
            {
                if (user.Email != null)
                {
                    if (verify == user.Password)
                    {
                        UserData.Add(user);
                        ViewBag.message = "Welcome, " + user.Username;
                        ViewBag.users = UserData.GetAll();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.message = "Passwords do not match";
                        ViewBag.user = user;
                        return View();
                    }
                }
                else
                {
                    ViewBag.message = "Email cannot be empty";
                    ViewBag.username = user.Username;
                    return View();
                }
            }
            else
            {
                ViewBag.message = "Username must be between 5 and 15 letters";
                if (user.Email != null) { ViewBag.email = user.Email; }
                return View();

            }
        }

        public IActionResult Detail(int userId)
        {
            ViewBag.user = UserData.GetById(userId);
            return View();
        }
    }
}