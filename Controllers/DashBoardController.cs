using Microsoft.AspNetCore.Mvc;
using Practico.Controllers.Attributes;
using Practico.DatabaseController;
using Practico.Models;

namespace Practico.Controllers
{
    [LoginAuth]
    public class DashBoardController : Controller
    {
        private readonly MyContext context;
        public tbUser tbUser { get; set; }
        public tbSurvey tbSurvey { get; set; }
        public DashBoardController(MyContext context)
        {
            this.context = context;
            tbUser = new tbUser(context);
            tbSurvey = new tbSurvey(context);
        }


        public IActionResult Index()
        {
            if (int.Parse(HttpContext.Session.GetString("IdRole")) == 3)
            {
                return RedirectToAction("Employee");
            }
            DateTime today = DateTime.Today;
            ViewBag.time = tbSurvey.GetLastSurveyDateByBoss(int.Parse(HttpContext.Session.GetString("UserId")));
            if (ViewBag.time < today)
                return RedirectToAction("BossFormPost");
            List<int> counter = new List<int>() { 0, 0, 0, 0, 0 };
            List<SurveyAnswerQuestion> surveyAnswerQuestions = context.SurveysAnswersQuestions.ToList();
            DashBoardModel model = new DashBoardModel();
            model.count = new List<int>();
            foreach (var item in surveyAnswerQuestions)
            {
                Survey s = context.Surveys.Where(x => x.Id == item.IdSurvey).FirstOrDefault();
                if (context.BossEmployees.Where(x => ((x.IdEmployee == s.IdEmployee) && (x.IdBoss == int.Parse(HttpContext.Session.GetString("UserId"))))).FirstOrDefault() != null)
                {
                    counter[item.Answer - 1]++;
                }

            }

            model.count = counter;
            model.Users = tbUser.EmployeesByBossId(int.Parse(HttpContext.Session.GetString("UserId")));
            return View(model);
        }

        [HttpGet]
        public IActionResult Employee()
        {
            ViewBag.time = tbSurvey.GetLastSurveyDateByEmployee(int.Parse(HttpContext.Session.GetString("UserId")));

            List<Question> list = context.Questions.Where(x => (x.ForEmployee == 1 && x.Default == 1)).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult BossFormPost()
        {
            ViewBag.time = tbSurvey.GetLastSurveyDateByBoss(int.Parse(HttpContext.Session.GetString("UserId")));

            List<Question> list = context.Questions.Where(x => (x.ForBoss == 1 && x.Default == 1)).ToList();
            return View("Employee", list);
        }

        [HttpGet]
        public IActionResult EmployeeMoodDetail(int IdUser)
        {
            List<Survey> su = context.Surveys.Where(x => x.IdEmployee == IdUser).ToList();
            List<SurveyAnswerQuestion> ass = new List<SurveyAnswerQuestion>();
            EmployeeDetail detail = new EmployeeDetail();
            detail.sum = new List<double>();
            detail.dateTimes = new List<string>();
            foreach (var item in su)
            {
                var sur = context.SurveysAnswersQuestions.Where(x => x.IdSurvey == item.Id).ToList();
                double s = 0;
                foreach (var question in sur)
                {
                    s += question.Answer;
                }
                if (sur.Count > 0)
                    s = s / sur.Count;

                detail.sum.Add(s);
                detail.dateTimes.Add(item.DateTime.ToString("MM/dd/yyyy HH:mm"));
            }
            return View(detail);
        }

        [HttpPost]
        public IActionResult FormPost(IFormCollection form)
        {
            if (int.Parse(HttpContext.Session.GetString("IdRole")) == 3)
            {
                tbSurvey.AddSuveyToEmployee(form, int.Parse(HttpContext.Session.GetString("UserId")));
            }
            else
            {
                tbSurvey.AddSuveyToEmployee(form, 0, int.Parse(HttpContext.Session.GetString("UserId")));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult logout()
        {
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.SetString("UserName", "");
            HttpContext.Session.SetString("IdRole", "");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult EmployeeEdit(int IdUser)
        {
            User? user = tbUser.GetUserById(IdUser);
            return View(user);
        }

        [HttpPost]
        public IActionResult EmployeeEdit(User user)
        {
            tbUser.EditEmployee(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddEmployes(int countEmp)
        {
            tbUser.AddMoreEmployeesToBoss(int.Parse(HttpContext.Session.GetString("UserId")), countEmp);
            return RedirectToAction("Index");
        }
    }
}
