using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
// for password validation login
using Microsoft.AspNetCore.Identity;
// For Sessions:
using Microsoft.AspNetCore.Http;

namespace C_Sharp_WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("")]
        public IActionResult CreateUser(User newUser)
        {
            UserWrapper Wrapper = new UserWrapper();
            Wrapper.NewUser = newUser;
            if(ModelState.IsValid)
            {
                User oldUser = dbContext.Users
                    .FirstOrDefault(u=>u.Email ==newUser.Email);
                if(oldUser != null)
                {
                    ModelState.AddModelError("NewUser.Email", "User with such email already exsits!");
                    return View("Index", Wrapper);
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password= Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        public IActionResult LoginUser(Login newLogin)
        {
            UserWrapper Wrapper = new UserWrapper();
            Wrapper.NewLogin = newLogin;
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u=> u.Email == newLogin.LoginEmail);
                if(userInDb == null)
                {
                    ModelState.AddModelError("NewLogin.LoginEmail", "User with such email does not exist");
                    return View("Index", Wrapper);
                }
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(newLogin, userInDb.Password, newLogin.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("NewLogin.LoginPassword", "Invalid Password");
                    return View("Index", Wrapper);
                }
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                int? temp = HttpContext.Session.GetInt32("UserId");
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                Console.WriteLine(temp);
                ViewBag.SessionUserId = temp;
                Console.Write("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                return View("Success");
            }
            return View("Index");

        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            var goSessions = HttpContext.Session.GetInt32("UserId");
            // ViewBag.SessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.SessionUserId = goSessions;
            Console.Write(goSessions);

            // if(goSessions!=null)
            return View("Success");
            // return View("FailedSessions");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
