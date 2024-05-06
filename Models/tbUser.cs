using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practico.Models
{
    [Table("tbUser")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public string? NewPassword { get; set; }

    }

    [Table("tbBossEmployee")]
    public class BossEmployee
    {
        [Key]
        public int Id { get; set; }
        public int IdBoss { get; set; }
        public int IdEmployee { get; set; }

    }

    public class EmployeeDetail
    {
        public List<double> sum { get; set; }
        public List<string> dateTimes { get; set; }
    }

    public class DashBoardModel
    {
        public List<int> count { get; set; }
        public List<User> Users { get; set; }
        public List<double> values { get; set; }
        public List<string> dates { get; set; }

    }

    [Table("tbRole")]
    public class Role
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
    }

    [Table("tbQuestion")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Default { get; set; }
        public int ForBoss { get; set; }
        public int ForEmployee { get; set; }
    }

    [Table("tbSurvey")]
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdBoss { get; set; }
    }

    [Table("tbSurveyAnswerQuestion")]
    public class SurveyAnswerQuestion
    {
        [Key]
        public int Id { get; set; }
        public int IdSurvey { get; set; }
        public int IdQuestion { get; set; }
        public int Answer { get; set; }
    }

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BossEmployee> BossEmployees { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<SurveyAnswerQuestion> SurveysAnswersQuestions { get; set; }
    }
}
