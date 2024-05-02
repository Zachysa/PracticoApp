using Practico.Models;

namespace Practico.DatabaseController
{
    public class tbSurvey
    {
        public readonly MyContext context;

        public tbSurvey(MyContext context)
        {
            this.context = context;
        }

        public DateTime GetLastSurveyDateByEmployee(int employeeId)
        {
            Survey? survey = context.Surveys.Where(x => x.IdEmployee == employeeId)
               .OrderByDescending(x => x.DateTime).FirstOrDefault();
            if (survey != null)
                return survey.DateTime;
            else
                return new DateTime();
        }

        public DateTime GetLastSurveyDateByBoss(int bossId)
        {
            Survey? survey = context.Surveys.Where(x => x.IdBoss == bossId)
               .OrderByDescending(x => x.DateTime).FirstOrDefault();
            if (survey != null)
                return survey.DateTime;
            else
                return new DateTime();
        }

        public void AddSuveyToEmployee(IFormCollection form, int employeeId = 0, int bossId = 0)
        {
            List<string> keys = form.Keys.ToList();
            Survey survey = new Survey();
            if (employeeId > 0)
                survey.IdEmployee = employeeId;
            else
                survey.IdBoss = bossId;
            survey.DateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            context.Surveys.Add(survey);
            context.SaveChanges();
            for (int i = 0; i < keys.Count - 1; i++)
            {
                string[] arr = keys[i].Split(';');
                int questionId = int.Parse(arr[0]);
                int answer = int.Parse(arr[1]);
                SurveyAnswerQuestion answerQuestion = new SurveyAnswerQuestion();
                answerQuestion.Answer = answer;
                answerQuestion.IdQuestion = questionId;
                answerQuestion.IdSurvey = survey.Id;
                context.SurveysAnswersQuestions.Add(answerQuestion);
                context.SaveChanges();
            }
        }
    }
}
