using Microsoft.AspNet.Identity;
using SimpleCommunityMessager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCommunityMessager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            var CurrentUser = db.Users.Find(User.Identity.GetUserId());

            var posts = from p in db.Posts
                        select p;

            posts = posts.Where(p => p.Receiver.Equals(CurrentUser));

            posts = posts.Where(p => p.Read.Equals(false));

            IndexDTO dto = new IndexDTO();

            //dto.unreadCounter = posts.Count();

            dto.unreadCounter = 2;

            dto.userName = CurrentUser.UserName;

            dto.lastLogin = CurrentUser.LastLogin;

            dto.loginCounter = CurrentUser.LoginCounter;

            return View(dto);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}