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
                return RedirectToAction("Dashboard");
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
                ViewBag.UserId =  HttpContext.Session.GetInt32("UserId");
                Console.Write("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                return RedirectToAction("Dashboard");
            }
            return View("Index");

        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            var goSessions = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            Console.Write(goSessions);
            return View("Success");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }



        // WEDDING PLANNER:

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            
            int? sessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null)
                return RedirectToAction("Success");
            List<Wedding> AllWeddings = dbContext.Weddings  
                .Include(w=>w.WeddingGuests)
                .ToList();


            return View("Dashboard", AllWeddings);
        }
        // WEDDING Create page and action to create wedding:

        [HttpGet("wedding/new")]
        public IActionResult CreateWedPage()
        {
            int? sessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null)
                return RedirectToAction("Success");
            return View("CreateWedPage");
        }

        [HttpPost("wedding/new")]
        public IActionResult CreateNewWedding(Wedding NewWedding)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (SessionUserId == null)
                return RedirectToAction("Success");

            if(ModelState.IsValid)
            {
                dbContext.Weddings.Add(NewWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");

            }
            return View("CreateWedPage");
        }


        [HttpGet("details/{WeddingId}")]
        public IActionResult Details(int WeddingId)
        {
            int? sessionUserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null)
                return RedirectToAction("Success");
            Wedding thisWedding = dbContext.Weddings
                .Include(w=>w.WeddingGuests)
                .ThenInclude(a=>a.ToBeGuest)
                .FirstOrDefault(w=>w.WeddingId == WeddingId);
            return View("Details", thisWedding);
        }

        [HttpGet("delete/{WeddingId}")]
        public IActionResult Delete(int WeddingId)
        {
            int? sessionUserId = HttpContext.Session.GetInt32("UserId");
            if (sessionUserId == null)
                return RedirectToAction("Success");
            Wedding weddingToDelete = dbContext.Weddings
                .SingleOrDefault(w=>w.WeddingId == WeddingId);
            dbContext.Weddings.Remove(weddingToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost("rsvp/{WeddingId}")]
        public IActionResult RSVP(int WeddingId)
        {
            int? SessionUserId = HttpContext.Session.GetInt32("UserId");
              if (SessionUserId == null)
                return RedirectToAction("Success");
            Association newAs = new Association();
            newAs.UserId = SessionUserId.GetValueOrDefault();
            newAs.WeddingId = WeddingId;
            dbContext.Add(newAs);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        // [HttpPost("{WeddingId}")] DO NOT USE IT WITH LINK asp-action
        public IActionResult UnRSVP(int WeddingId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId == null)
                return RedirectToAction("Success");
            Association AsToDelete = dbContext.Associations
                .FirstOrDefault(a=>a.WeddingId == WeddingId && a.UserId == UserId);
            dbContext.Associations.Remove(AsToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
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
