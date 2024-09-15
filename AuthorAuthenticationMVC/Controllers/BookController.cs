using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorAuthenticationMVC.Data;
using AuthorAuthenticationMVC.Models;
using NHibernate.Criterion;

namespace AuthorAuthenticationMVC.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult GetBook()
        {
            var target = (Guid)Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Books>().Where(b => b.Author.Id==target).ToList();
                return View(books);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(Books book) {

            var authId = Session["AuthorId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var author = session.Get<Author>((Guid)authId);
                    book.Author = author;
                    session.Save(book);
                    txn.Commit();
                    return RedirectToAction("GetBook",new { authId = (Guid)authId });
                }
            }
        
        }

        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction()) {

                    var book = session.Get<Books>(id);
                    return View(book);
                }
            }
        }


        [HttpPost]

        public ActionResult Edit(Books book)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var targetBook = session.Get<Books>(book.Id);
                    targetBook.Name = book.Name;
                    targetBook.Genre = book.Genre;
                    targetBook.Description = book.Description;
                    session.Update(targetBook);
                    txn.Commit();
                    return RedirectToAction("GetBook");

                }
            }
        }

            public ActionResult Delete(int id)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {

                        var book = session.Get<Books>(id);
                        return View(book);
                    }
                }
            }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteBook(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var book = session.Get<Books>(id);
                    session.Delete(book);
                    txn.Commit();
                    return RedirectToAction("GetBook");
                }
            }
        }
        
        }


        

        
    }
