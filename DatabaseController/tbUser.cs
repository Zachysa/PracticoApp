using Practico.Models;

namespace Practico.DatabaseController
{
    public class tbUser
    {
        public readonly MyContext _dbContext;
        public tbUser(MyContext context)
        {
            _dbContext = context;
        }

        public User? GetUserByNickNameAndPassword(User user)
        {
            return _dbContext.Users.Where(a => a.Name.Equals(user.Name) && a.Password.Equals(user.Password)).FirstOrDefault();
        }

        public User? GetUserById(int id)
        {
            return _dbContext.Users.Where(a => a.Id == id).FirstOrDefault();
        }


        public List<User?> EmployeesByBossId(int bossId)
        {
            List<BossEmployee>? users = _dbContext.BossEmployees.Where(x => x.IdBoss == bossId).ToList();
            List<User?> emp = new List<User?>();
            for (int i = 0; i < users.Count; i++)
            {
                emp.Add(_dbContext.Users.Where(x => x.Id == users[i].IdEmployee).FirstOrDefault());
            }
            return emp;
        }

        public void AddMoreEmployeesToBoss(int bossId, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Random random = new Random();
                byte[] bytes = new byte[4];
                random.NextBytes(bytes);

                string hexString = BitConverter.ToString(bytes).Replace("-", "");
                User user = new User();
                user.Name = hexString;
                user.Password = hexString;
                user.IdRole = 3;
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                BossEmployee be = new BossEmployee();
                be.IdEmployee = user.Id;
                be.IdBoss = bossId;
                _dbContext.BossEmployees.Add(be);
                _dbContext.SaveChanges();
            }
        }

        public void EditEmployee(User user)
        {
            user.IdRole = 3;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
    }
}
