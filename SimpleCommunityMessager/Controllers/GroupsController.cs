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
    [Authorize]
    public class GroupsController : Controller
    {
        //private GroupDBContext db = new GroupDBContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            List<Group> groupList = db.Groups.ToList();
            List<GroupUser> groupUserlist = db.GroupUsers.Where(g => g.User.Id == currentUser.Id).ToList();
            List<GroupListViewModel> groupsDTO = new List<GroupListViewModel>();

            foreach (var item in groupList)
            {
                GroupListViewModel dto = new GroupListViewModel();
                dto.Member = false;
                dto.Id = item.Id;
                dto.Name = item.Name;

                foreach (var item2 in groupUserlist)
                {
                    if (item.Id.Equals(item2.Group.Id))
                    {
                        dto.Member = true;
                    }
                }
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
        public ActionResult Create([Bind(Include = "Id,Name")] CreateGroupViewModel newGroup)
        {
            if (ModelState.IsValid)
            {
                Group group = new Group();
                group.Name = newGroup.Name;
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newGroup);
        }

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
