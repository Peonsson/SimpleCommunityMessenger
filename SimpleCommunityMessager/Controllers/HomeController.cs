﻿using Microsoft.AspNet.Identity;
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
            AccountSummaryViewModel dto = new AccountSummaryViewModel();

            var CurrentUser = db.Users.Find(User.Identity.GetUserId());
            
            var count = db.Posts.Count(t => t.Receiver.Id == CurrentUser.Id && t.Read == false);

            dto.UnreadCounter = count;

            dto.Username = CurrentUser.UserName;

            dto.LastLogin = CurrentUser.LastLogin;

            dto.LoginCounter = CurrentUser.LoginCounter;

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