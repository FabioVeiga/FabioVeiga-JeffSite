
namespace JeffSite_WF_472.Models
{
    public class UserLogged
    {
        public string UserName { get; set; }

        public bool IsUserLogged()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            return true;
        }
    }
}