using JeffSite_WF_472.Data;
using JeffSite_WF_472.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace JeffSite_WF_472.Services
{
    public class UserService
    {
        private readonly JeffContext _context;

        private readonly IList<Claim> _usersLogged;

        public UserService(JeffContext context)
        {
            _context = context;
        }
        public bool ValidateUser(User user){
            string senhaEncriptada = Utils.Util.GerarHashMd5(user.Pass);
            if(senhaEncriptada == "erro:senha-vazia"){
                return false;
            }
            return  _context.User.Any(u => u.UserName == user.UserName && u.Pass == senhaEncriptada);
        }

        public User GetUserBYLogin(string login){
            return _context.User.FirstOrDefault(u => u.UserName == login);
        }

        public void ChangePassword(User user){
            user.Pass = Utils.Util.GerarHashMd5(user.Pass);
            _context.User.Update(user);
            _context.SaveChanges();
        }

    }

}
