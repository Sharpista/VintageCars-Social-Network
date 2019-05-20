using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheSocialNetwork.DataAccess.Contexts;
using TheSocialNetwork.DomainModel.Entities;

namespace TheSocialNetwork.WebApp.Controllers
{
    public class MarketPlacesController : Controller
    {
        private SocialNetworkContext db = new SocialNetworkContext();

        // GET: MarketPlaces
        public ActionResult Index()
        {
            return View(db.MarketPlaces.ToList());
        }

        public ActionResult JoinGroup(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guid profileId = Guid.Parse(Session["profileId"].ToString());

            //Adiciono um registro ProfileGroup na tabela ProfilesGroups
            //contendo meu Profile e meu MarketPlace
            var profileGroup = new ProfileGroup();
            profileGroup.Id = Guid.NewGuid();
            profileGroup.Profile = db.Profiles.Find(profileId);
            profileGroup.MarketPlace = db.MarketPlaces.Find(id);
            //Adiciono o novo registro/tupla no banco de dados
            db.ProfilesGroups.Add(profileGroup);
            db.SaveChanges();

            return RedirectToAction("Details", new { id });
        }

        // GET: MarketPlaces/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketPlace group = db.MarketPlaces.Find(id);

            Session["groupId"] = id;

            if (group == null)
            {
                return HttpNotFound();
            }

            //Pego todas as Postagens que foram feitas neste grupo
            ViewBag.Posts = db.Posts
                .Include(p => p.Sender)
                .Include(p => p.MarketPlace)
                .Where(p => p.MarketPlace != null)
                .Where(p => p.MarketPlace.Id == id).ToList();

            //Pego todos os comentários por PostId do Banco de Dados
            var comments = db.Comments
                .Include(c => c.Post)
                .Include(c => c.Post.Sender)
                .OrderBy(p => p.PublishDateTime)
                .ToList();
            var postIdComments = new Dictionary<Guid, List<Comment>>();
            foreach(var comment in comments)
            {
                Guid postId = comment.Post.Id;
                if (!postIdComments.ContainsKey(comment.Post.Id))
                    postIdComments[postId] = new List<Comment>();
                postIdComments[postId].Add(comment);

            }
            foreach(var postIdComment in postIdComments)
                postIdComment.Value.Sort((a, b) => a.PublishDateTime.CompareTo(b.PublishDateTime));
            ViewBag.PostIdComments = postIdComments;

            //Pego todos os membros de um grupo
            ViewBag.Members = db.ProfilesGroups
                .Include(p => p.Profile)
                .Include(p => p.MarketPlace)
                .Where(p => p.MarketPlace.Id == id)
                .Select(p => p.Profile).ToList();

            return View(group);
        }

        // GET: MarketPlaces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] MarketPlace group)
        {
            if (ModelState.IsValid)
            {
                group.Id = Guid.NewGuid();
                db.MarketPlaces.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: MarketPlaces/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketPlace group = db.MarketPlaces.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: MarketPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] MarketPlace group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: MarketPlaces/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketPlace group = db.MarketPlaces.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: MarketPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MarketPlace group = db.MarketPlaces.Find(id);
            db.MarketPlaces.Remove(group);
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
