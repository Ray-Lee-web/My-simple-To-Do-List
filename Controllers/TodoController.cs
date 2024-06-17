using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TodoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private int GetLoggedInUserId()
        {
            if (Session["UserId"] != null)
            {
                return (int)Session["UserId"];
            }
            return 0; // Or handle unauthenticated user scenario
        }

        public ActionResult Index()
        {
            int userId = GetLoggedInUserId();
            var todos = db.TodoItems.Where(t => t.UserId == userId).ToList();
            return View(todos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                todo.UserId = GetLoggedInUserId();
                db.TodoItems.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        public ActionResult Edit(int id)
        {
            var todo = db.TodoItems.Find(id);
            if (todo == null || todo.UserId != GetLoggedInUserId())
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public ActionResult Edit(TodoItem todo)
        {
            if (ModelState.IsValid)
            {
                var updateItem = db.TodoItems.Find(todo.TodoItemId);
                updateItem.Title = todo.Title;
                updateItem.Description = todo.Description;
                updateItem.DueDate = todo.DueDate;
                updateItem.Priority = todo.Priority;
                updateItem.IsComplete = todo.IsComplete;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        public ActionResult Delete(int id)
        {
            var todo = db.TodoItems.Find(id);
            if (todo == null || todo.UserId != GetLoggedInUserId())
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var todo = db.TodoItems.Find(id);
            db.TodoItems.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}