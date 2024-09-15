using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthorAuthenticationMVC.Data;
using AuthorAuthenticationMVC.Models;
using AuthorAuthenticationMVC.ViewModels;

namespace AuthorAuthenticationMVC.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]

        public ActionResult LogIn(LoginVM author)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var targetAuthor = session.Query<Author>().FirstOrDefault(a => a.Name == author.AuthorName);

                    if (targetAuthor != null)
                    {
                        Session["AuthorId"] = targetAuthor.Id;
                        if (BCrypt.Net.BCrypt.Verify(author.Password,targetAuthor.Password))
                        {
                            FormsAuthentication.SetAuthCookie(author.AuthorName, true);
                            return RedirectToAction("Index","Author");
                        }
                    }
                    ModelState.AddModelError("", "AuthorName/Password doesn't exists");
                    return View(author);
                }
            }
        }

        public ActionResult GetAllBooks()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Books>().ToList();
                return View(books);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(Author author)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    author.AuthorDetails.Author = author;
                    author.Password = BCrypt.Net.BCrypt.HashPassword(author.Password);
                    session.Save(author);
                    txn.Commit();
                    return RedirectToAction("LogIn");
                }
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
    }
}