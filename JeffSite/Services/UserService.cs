using System.Linq;
using System.Threading.Tasks;
using JeffSite.Data;
using JeffSite.Models;

namespace JeffSite.Services
{
    public class UserService
    {
        private readonly JeffContext _context;

        public UserService(JeffContext context)
        {
            _context = context;
        }
        public bool ValidateUser(User user){
            string senhaEncriptada = JeffSite.Utils.Util.GerarHashMd5(user.Pass);
            return  _context.User.Any(u => u.UserName == user.UserName && u.Pass == senhaEncriptada);
        }

        public User GetUserBYLogin(string login){
            return _context.User.FirstOrDefault(u => u.UserName == login);
        }

        public void ChangePassword(User user){
            _context.User.Update(user);
            _context.SaveChanges();
        }

    }

}
