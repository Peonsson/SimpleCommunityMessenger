using System;
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
    public class GroupsController : Controller
    {
        //private GroupDBContext db = new GroupDBContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            List<Group> groups = db.Groups.ToList();
            List<GroupListViewModel> groupsDTO = new List<GroupListViewModel>();

            foreach(var item in groups)
            {
                GroupListViewModel dto = new GroupListViewModel();
                dto.Id = item.Id;
                dto.Name = item.Name;
                groupsDTO.Add(dto);
            }

            return View(groupsDTO);
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReceivedGroupPostSummaryViewModel dto = new ReceivedGroupPostSummaryViewModel();
            dto.Id = (int) id;

            List<GroupMessage> mcp = db.GroupMessages.Where(p => p.Group.Id == id).ToList();


            List<ReceivedGroupPostBriefViewModel> dtoList = new List<ReceivedGroupPostBriefViewModel>();
            foreach (GroupMessage item in mcp)
            {
                ReceivedGroupPostBriefViewModel messageDetails = new ReceivedGroupPostBriefViewModel();

                messageDetails.Id = item.Id;
                messageDetails.Subject = item.Subject;
                messageDetails.Timestamp = item.Timestamp;

                dtoList.Add(messageDetails);
            }
            dto.messages = dtoList;

            return View(dto);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }


        /* IN PRODUCTION */
        /* IN PRODUCTION */
        /* IN PRODUCTION */


        // GET: Groups/Join/5
        public ActionResult Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            var CurrentUser = db.Users.Find(User.Identity.GetUserId());
            var GroupToJoin = db.Groups.Find(id);

            // Create group user
            var newGroupUser = new GroupUser();
            newGroupUser.Group = GroupToJoin;
            newGroupUser.User = CurrentUser;

            var foundGroupUser = db.GroupUsers.Where(g => g.User.Id == newGroupUser.User.Id && g.Group.Id == newGroupUser.Group.Id).FirstOrDefault();

            if (foundGroupUser == null)
            {
                // User-group relationship didn't already exists, add new
                db.GroupUsers.Add(newGroupUser);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: Groups/Join/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Leave/5
        public ActionResult Leave(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            var CurrentUser = db.Users.Find(User.Identity.GetUserId());

            try
            {
                var groupUser = db.GroupUsers.Where(u => u.User.Id == CurrentUser.Id && u.Group.Id == id).FirstOrDefault();
                db.GroupUsers.Remove(groupUser);
                db.SaveChanges();
            }
            catch (ArgumentNullException) {
                return RedirectToAction("Index");
            } 
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        // POST: Groups/Leave/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }



        /* END */
        /* END */
        /* END */



        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
