using System;
using System.Linq;
using System.Web.Mvc;
using OnlineQuiz.Models;

namespace OnlineQuiz.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            QuizContext db = new QuizContext();

            var result = (from q in db.Questions
                         orderby Guid.NewGuid()
                         where q.SubjectID==8
                         select q).Take(2);

            return View(result);
        }
    }
}