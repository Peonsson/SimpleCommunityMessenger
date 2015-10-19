﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCommunityMessager.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace SimpleCommunityMessager.Controllers
{
    public class GroupMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MulticastPosts
        public ActionResult Index()
        {
            return View(db.GroupMessages.ToList());
        }

        // GET: MulticastPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessage multicastPost = db.GroupMessages.Find(id);
            if (multicastPost == null)
            {
                return HttpNotFound();
            }

            // Create view model
            GroupMessageViewModel post = new GroupMessageViewModel();
            post.Id = multicastPost.Id;
            post.Subject = multicastPost.Subject;
            post.Timestamp = multicastPost.Timestamp;
            post.Message = multicastPost.Message;

            return View(post);
        }

        // GET: MulticastPosts/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.GroupId = id;

            return View();
        }

        // POST: MulticastPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject,Message,ReceiverGroup")] CreateGroupMessageViewModel newMcp)
        {
            Debug.WriteLine("id = " + newMcp.ReceiverGroup);

            if (ModelState.IsValid)
            {
                GroupMessage newPost = new GroupMessage();

                newPost.Subject = newMcp.Subject;

                newPost.Message = newMcp.Message;

                newPost.Sender = db.Users.Find(User.Identity.GetUserId());

                newPost.Group = db.Groups.Where(u => u.Id == newMcp.ReceiverGroup).FirstOrDefault();

                newPost.Timestamp = DateTime.Now;

                db.GroupMessages.Add(newPost);
                db.SaveChanges();
                return RedirectToAction("Details/" + newMcp.ReceiverGroup, "Groups");
            }

            return View(newMcp);
        }

        // GET: MulticastPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessage multicastPost = db.GroupMessages.Find(id);
            if (multicastPost == null)
            {
                return HttpNotFound();
            }
            return View(multicastPost);
        }

        // POST: MulticastPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,Message,Timestamp,Deleted")] GroupMessage multicastPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(multicastPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(multicastPost);
        }

        // GET: MulticastPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessage multicastPost = db.GroupMessages.Find(id);
            if (multicastPost == null)
            {
                return HttpNotFound();
            }
            return View(multicastPost);
        }

        // POST: MulticastPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupMessage multicastPost = db.GroupMessages.Find(id);
            db.GroupMessages.Remove(multicastPost);
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
