using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userdb.Models;

namespace userdb.Controllers
{
    public class WallController : Controller
    {
        private UserDBContext _context;
        public WallController(UserDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("users/show/{UserId}")]
        public IActionResult ShowUser(int UserId)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "User");
            User EditUser = _context.Users

                            .Include(r => r.ReceivedMessages)
                                .ThenInclude(r => r.Sender)
                                .ThenInclude(r => r.Comments)
                            .SingleOrDefault(r => r.UserId == UserId);

            ViewBag.User = EditUser;
            
            return View("Show");
        }

        [HttpPost]
        [Route("Wall/PostMessage")]
        [ValidateAntiForgeryToken]
        public IActionResult PostMessage(string message, int UserId)
        {
            Message newMessage = new Message();
            newMessage.MessageText = message;
            newMessage.ReceiverUserId = UserId;
            newMessage.SenderUserId = (int)HttpContext.Session.GetInt32("UserId");
            newMessage.CreatedAt = DateTime.Now;
            newMessage.UpdatedAt = DateTime.Now;

            _context.Messages.Add(newMessage);
            _context.SaveChanges();

            return RedirectToAction("ShowUser", new { UserId = UserId });
        }

        [HttpPost]
        [Route("Wall/PostComment")]
        [ValidateAntiForgeryToken]
        public IActionResult PostComment(string comment, int MessageId, int UserId)
        {
            Comment newComment = new Comment();
            newComment.MessageId = MessageId;
            newComment.CommentText = comment;
            newComment.UserId = (int)HttpContext.Session.GetInt32("UserId");
            newComment.CreatedAt = DateTime.Now;
            newComment.UpdatedAt = DateTime.Now;

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return RedirectToAction("ShowUser", new { UserId = UserId });
        }

        private bool IsUserLoggedIn()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0 ) {
                return false;
            } else {
                return true;
            }
        }
    }
}
