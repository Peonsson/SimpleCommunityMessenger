using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SimpleCommunityMessager.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace SimpleCommunityMessager.Controllers
{
    [Authorize]
    public class GroupMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupMessages
        public ActionResult Index()
        {
            return View(db.GroupMessages.ToList());
        }

        // GET: GroupMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessage groupMessage = db.GroupMessages.Find(id);
            if (groupMessage == null)
            {
                return HttpNotFound();
            }

            // Create view model
            GroupMessageViewModel post = new GroupMessageViewModel();
            post.Id = groupMessage.Id;
            post.Subject = groupMessage.Subject;
            post.Timestamp = groupMessage.Timestamp;
            post.Message = groupMessage.Message;

            return View(post);
        }

        // GET: GroupMessages/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.GroupId = id;

            return View();
        }

        // POST: GroupMessages/Create
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
    }
}
