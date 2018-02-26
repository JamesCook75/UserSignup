using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSignup.Models;
using UserSignup.ViewModels;

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
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User newUser = addUserViewModel.NewUser(
                    addUserViewModel.Username,
                    addUserViewModel.Email,
                    addUserViewModel.Password
                    );

                UserData.Add(newUser);

                return Redirect("/");
            }

            return View(addUserViewModel);
        }

        public IActionResult Detail(int userId)
        {
            ViewBag.user = UserData.GetById(userId);
            return View();
        }
    }
}