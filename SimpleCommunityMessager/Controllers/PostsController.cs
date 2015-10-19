﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleCommunityMessager.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace SimpleCommunityMessager.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var CurrentUser = db.Users.Find(User.Identity.GetUserId());
            ReceivedPostsOverviewViewModel dto = new ReceivedPostsOverviewViewModel();

            dto.Usernames = db.Posts.Where(p => p.Receiver.Id == CurrentUser.Id && p.Deleted == false).Select(p => p.Sender.UserName).Distinct().ToList();

            dto.TotalMessages = db.Posts.Count(t => t.Receiver.Id == CurrentUser.Id);

            dto.ReadMessages = db.Posts.Count(t => t.Receiver.Id == CurrentUser.Id && t.Read == true);

            dto.DeletedMessages = db.Posts.Count(t => t.Receiver.Id == CurrentUser.Id && t.Deleted == true);

            return View(dto);

        }

        // GET: Posts/Details/5
        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ReceivedPostSummaryViewModel> dtoList = new List<ReceivedPostSummaryViewModel>();
        
            var CurrentUser = db.Users.Find(User.Identity.GetUserId());

            List<Post> posts = db.Posts.Where(p => p.Sender.UserName == username && p.Receiver.Id == CurrentUser.Id && p.Deleted == false).ToList();

            foreach(var item in posts)
            {
                ReceivedPostSummaryViewModel dto = new ReceivedPostSummaryViewModel();
                dto.Timestamp = item.Timestamp;
                dto.Subject = item.Subject;
                dto.Id = item.Id;
                dtoList.Add(dto);
            }

            return View(dtoList);
        }

        // GET: Posts/MessageDetails/5
        public ActionResult MessageDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            post.Read = true;

            // Create view model
            ReceivedPostDetailsViewModel readPostDTO = new ReceivedPostDetailsViewModel();
            readPostDTO.Id = post.Id;
            readPostDTO.Subject = post.Subject;
            readPostDTO.Timestamp = post.Timestamp;
            readPostDTO.Message = post.Message;

            db.SaveChanges();
            return View(readPostDTO);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            var UserList = new List<string>();

            var UserQuery = from d in db.Users
                            orderby d.UserName
                            select d.UserName;

            UserList.AddRange(UserQuery.Distinct());
            ViewBag.Receiver = new SelectList(UserList);

            if (TempData["successMessage"] != null)
            {
                // If there was a message in TempData, put message in viewbag
                ViewBag.SuccessMessage = TempData["successMessage"].ToString();
            }

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject,Message,Receiver")] CreateNewPostViewModel post)
        {
            if (ModelState.IsValid)
            {
                Post newPost = new Post();

                newPost.Subject = post.Subject;
                newPost.Message = post.Message;
                newPost.Timestamp = DateTime.Now;
                newPost.Read = false;
                newPost.Deleted = false;

                // Get ApplicationUser object of sender and receiver and add to newPost
                var CurrentUser = db.Users.Find(User.Identity.GetUserId());
                var receiver = db.Users.Where(u => u.UserName == post.Receiver).FirstOrDefault();
                newPost.Sender = CurrentUser;
                newPost.Receiver = receiver;

                db.Posts.Add(newPost);
                db.SaveChanges();

                // Put a message in TempData so the GET method of Create can put the message in the viewbag for the view
                TempData["successMessage"] = "Message was sent to " + newPost.Receiver.UserName + " at " + newPost.Timestamp;

                return RedirectToAction("Create");
            }

            return View();
        }

        // GET: Posts/CreateMulticast
        public ActionResult CreateMulticast()
        {
            return View();
        }

        // POST: Posts/CreateMulticast
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMulticast([Bind(Include = "Subject,Message,Receivers")] CreateNewMulticastPostViewModel post)
        {
            if (ModelState.IsValid)
            {

                string[] parts = post.Receivers.Split(' ');

                if (parts.Length > 0)
                {
                    foreach (string part in parts)
                    {
                        Post newPost = new Post();

                        newPost.Subject = post.Subject;
                        newPost.Message = post.Message;
                        newPost.Timestamp = DateTime.Now;
                        newPost.Read = false;
                        newPost.Deleted = false;

                        var CurrentUser = db.Users.Find(User.Identity.GetUserId());
                        newPost.Sender = CurrentUser;
                        var receiver = db.Users.Where(u => u.UserName == part).FirstOrDefault();

                        if(receiver == null)
                        {
                            continue;
                        }

                        newPost.Receiver = receiver;

                        db.Posts.Add(newPost);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            return View();
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);

            post.Read = true;
            post.Deleted = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
