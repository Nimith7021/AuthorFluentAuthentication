﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthorAuthenticationMVC.Data;
using AuthorAuthenticationMVC.Models;
using NHibernate.Linq;

namespace AuthorFluentMVC.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author

        public ActionResult Index()
        {
            if (Session["AuthorId"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("LogIn", "User");
            }
            Guid guid = (Guid)Session["AuthorId"];
            ViewBag.guid = guid;
            return View();
        }

        public ActionResult GetDetails(Guid authId)
        {
            
            
            using (var session = NHibernateHelper.CreateSession())
            {
                var details = session.Query<AuthorDetails>().FirstOrDefault(ad => ad.Author.Id == authId);
                if (details == null)
                {
                    return RedirectToAction("Create");
                }
                return View(details);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(AuthorDetails authorDetails)
        {
            var id = Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var author = session.Query<Author>().SingleOrDefault(a=>a.Id==(Guid)id);
                    authorDetails.Author = author;
                    session.Save(authorDetails);
                    txn.Commit();
                    return RedirectToAction("GetDetails", new { authId = (Guid)id });
                }
            }
        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var detail = session.Get<AuthorDetails>(id);
                return View(detail);
            }
        }

        [HttpPost]

        public ActionResult Edit(AuthorDetails authorDetails)
        {
            var guid = Session["AuthorId"];
            
            using (var session = NHibernateHelper.CreateSession())
            {
                    using (var txn = session.BeginTransaction()) {
                    var target = session.Get<Author>((Guid)guid);
                    authorDetails.Author = target;
                    session.Evict(target);
                    session.Update(authorDetails);
                    txn.Commit();
                    return RedirectToAction("GetDetails", new { authId = (Guid)guid });
                }
            }
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var targetDetails = session.Get<AuthorDetails>(id);
                return View(targetDetails);
            }
        }

        [HttpPost,ActionName("Delete")]

        public ActionResult DeleteDetails(int id)
        {
            var guid = Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var target = session.Get<AuthorDetails>(id);
                    session.Delete(target);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        


        











    }
}

