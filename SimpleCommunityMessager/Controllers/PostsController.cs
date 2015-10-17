using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleCommunityMessager.Models;
using Microsoft.AspNet.Identity;

namespace SimpleCommunityMessager.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var CurrentUser = db.Users.Find(User.Identity.GetUserId());
            return View(db.Posts.Where(o => o.Receiver.Id == CurrentUser.Id).Distinct().ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Post post = db.Posts.Find(id);
            string senderId = post.Sender.Id;



            //if (post == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(post);

            return View(db.Posts.Where(o => o.Sender.Id == senderId).ToList());
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

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject,Message,Receiver")] CreatePostDTO post)
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
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,Message,Timestamp,Read,Deleted")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
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
            db.Posts.Remove(post);
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
