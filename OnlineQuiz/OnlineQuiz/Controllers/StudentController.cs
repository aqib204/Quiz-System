using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineQuiz.Models;

namespace OnlineQuiz.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Login login)
        {
            QuizContext quizContext = new QuizContext();

            var userDetails = quizContext.Logins
                .Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();

            if (userDetails == null)
            {
                login.loginErrorMessage = "Wrong username or password";
                return View("Index", login);
            }
            else
            {
                Session["UserID"] = userDetails.UserID;
                Session["Username"] = login.Username;
                return RedirectToAction("Index", "StudentHome");
            }
        }
    }
}