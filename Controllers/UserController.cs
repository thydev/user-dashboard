using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using userdb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace userdb.Controllers
{
    public class UserController : Controller
    {
        private UserDBContext _context;
        public UserController(UserDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Index()
        {
            return View("Register");
        }

        [HttpGet]
        [Route("users/new")]
        public IActionResult New()
        {
            return View("Add");
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel item)
        {
            if (_context.Users.Count() > 0) {
                if(_context.Users.Any(r => r.Email.ToLower() == item.Email.ToLower()))
                {
                    ModelState.AddModelError("Email", "Email address already exists. Please enter a different email address.");
                }
            }

            // As soon as the model is submitted TryValidateModel() is run for us, ModelState is already set
            if(ModelState.IsValid)
            {
                // Handle Success Case
                int level = 2; // Normal
                if(_context.Users.Count() == 0) {
                    level = 1; // Admin
                }
                User newUser = new User {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Password = item.Password,
                    Level = level,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
    
                _context.Users.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                
                // if(newUser.Level == 1) return RedirectToAction("DashboardAdmin");

                return RedirectToAction("Dashboard");
            }
            return View("Register", item);
        }

        [HttpPost]
        [Route("CreateUser")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserViewModel item)
        {
            if (_context.Users.Count() > 0) {
                if(_context.Users.Any(r => r.Email.ToLower() == item.Email.ToLower()))
                {
                    ModelState.AddModelError("Email", "Email address already exists. Please enter a different email address.");
                }
            }

            // As soon as the model is submitted TryValidateModel() is run for us, ModelState is already set
            if(ModelState.IsValid)
            {
                // Handle Success Case
                int level = 2; // Normal
                if(_context.Users.Count() == 0) {
                    level = 1; // Admin
                }
                User newUser = new User {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Password = item.Password,
                    Level = level,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
    
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View("Register", item);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("VerifyLogin")]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyLogin(string Email, string Password)
        {
            string PasswordToCheck = Password;
            // Attempt to retrieve a user from your database based on the Email submitted
            var user = _context.Users.SingleOrDefault(r => r.Email == Email);
            if(user != null && PasswordToCheck != null)
            {
                var Hasher = new PasswordHasher<User>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, PasswordToCheck))
                {
                    //Handle success
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    // if (user.Level == 1) {
                    //     return RedirectToAction("DashboardAdmin");
                    // }
                    return RedirectToAction("Dashboard");
                }
            }

            //Handle failure
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            IEnumerable<User> AllUsers = _context.Users.OrderBy(r => r.FullName).ToList();
            User CurrentUser = _context.Users.SingleOrDefault(r => r.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.Users = AllUsers;
            if (CurrentUser.Level == 1) {
                return View("DashboardAdmin");
            }
            return View("Dashboard");
        }
        
        [HttpGet]
        [Route("dashboard/admin")]
        public IActionResult DashboardAdmin()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            IEnumerable<User> AllUsers = _context.Users.OrderBy(r => r.FullName).ToList();
            // User CurrentUser = _context.Users.SingleOrDefault(r => r.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.Users = AllUsers;
            return View("DashboardAdmin");
        }

        [HttpGet]
        [Route("users/show/{UserId}")]
        public IActionResult Show(int UserId)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");

            User TheUser = _context.Users.SingleOrDefault(r => r.UserId == UserId);
            ViewBag.User = TheUser;
            return View("Show");
        }

        [HttpGet]
        [Route("users/edit/{UserId}")]
        public IActionResult Edit(int UserId)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");

            User EditUser = _context.Users.SingleOrDefault(r => r.UserId == UserId);
            ViewBag.User = EditUser;
            return View("Edit");
        }

        [HttpGet]
        [Route("users/edit")]
        public IActionResult Edit()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");

            User EditUser = _context.Users.SingleOrDefault(r => r.UserId == HttpContext.Session.GetInt32("UserId"));
            ViewBag.User = EditUser;
            return View("Edit");
        }
        private bool IsUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0 ) {
                return false;
            } else {
                return true;
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
