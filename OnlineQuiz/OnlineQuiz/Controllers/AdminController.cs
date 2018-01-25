using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineQuiz.Models;

namespace OnlineQuiz.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewUsersRecord(string searchTerm)
        {
            QuizContext db = new QuizContext();
            var result = db.UserQuizRecords;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                var fresult = result.Where(x => x.Username.Contains(searchTerm));
                return View(fresult);
            }
            return View(result);
        }

        public ActionResult GetStudents(string term)
        {
            QuizContext db = new QuizContext();
            var result = db.UserQuizRecords;
            List<string> students = result.Where(s => s.Username.Contains(term))
                .Take(5)
                .Select(x => x.Username).ToList();

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? id)
        {
            QuizContext db = new QuizContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserQuizRecord record = db.UserQuizRecords.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizContext db = new QuizContext();
            UserQuizRecord record = db.UserQuizRecords.Find(id);
            db.UserQuizRecords.Remove(record);
            db.SaveChanges();

            return RedirectToAction("ViewUsersRecord");
        }

    }
}