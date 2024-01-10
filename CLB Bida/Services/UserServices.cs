using CLB_Bida.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Services
{
   public class UserServices
    {
        public bool UserAuthenticate(string username, string password)
        {
            using (var context = new BilliardContext())
            {
                return context.UserAccounts.Any(x => x.Username.Trim() == username.Trim() && x.Password.Trim() == password.Trim());
            }
        }
    }
}
