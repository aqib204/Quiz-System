using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using OnlineQuiz.Models;

namespace OnlineQuiz.Controllers
{
    public class StudentHomeController : Controller
    {
        public ActionResult Index()
        {
            QuizContext db = new QuizContext();
            int user_id = (int) Session["UserID"];

            var user_record = db.UserQuizRecords.Where(x => x.UserID == user_id);
            return View(user_record);
        }

        public ActionResult ViewAllSubjects()
        {
            QuizContext db = new QuizContext();
            return View(db.Subjects.ToList());
        }

        public ActionResult ViewAllQuestions(int? id)
        {
            QuizContext db = new QuizContext();
            //var questions = db.Questions.Where(q => q.SubjectID==id);

            var questions = (from q in db.Questions
                orderby Guid.NewGuid()
                where q.SubjectID==id
                select q).Take(5);

            var sname = db.Subjects.Where(q => q.SubjectID == id).SingleOrDefault();
            Session["SubjectName"] = sname.SubjectName;
            return View(questions);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Result(string option1, string option2, string option3, string option4, string option5)
        {
            int correct_answers = 0;
            int count = 0, temp=0;

            //Following list contains questionID for retrieving questions from DB
            List<int> questionsids = new List<int>()
            {
                (int)Session["QID1"],
                (int)Session["QID2"],
                (int)Session["QID3"],
                (int)Session["QID4"],
                (int)Session["QID5"]
            };

            //Following list contains list of options user have selected
            List<String> option_list = new List<string>()
            {
                option1, option2, option3, option4, option5
            };

            QuizContext context = new QuizContext();

            foreach (var item in option_list)
            {
                temp = questionsids[count];
                var result = context.Questions.Where(x => x.CorrectOption == item && x.QuestionID == temp).FirstOrDefault();
                count += 1;

                if (result != null)
                {
                    correct_answers += 1;
                }
            }

            UserQuizRecord userrecord = new UserQuizRecord();
            userrecord.UserID = (int) Session["UserID"];
            userrecord.Username = (string) Session["Username"];
            userrecord.SubjectName = (string) Session["SubjectName"];
            userrecord.QuizDate = DateTime.Now;
            userrecord.MarksObtained = correct_answers;

            context.UserQuizRecords.Add(userrecord);
            context.SaveChanges();
            ViewBag.correctAnswers = correct_answers;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Student");
        }
    }
}