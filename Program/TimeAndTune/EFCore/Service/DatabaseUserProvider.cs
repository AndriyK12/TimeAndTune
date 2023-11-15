using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Service
{
    internal class DatabaseUserProvider : IUserProvider
    {
        public int getCoinsAmount(User user)
        {
            return (int)user.Coinsamount;
        }

        public string getEmail(User user)
        {
            return user.Email;
        }

        public int getUserID(User user)
        {
            return user.Userid;
        }

        public string getUserName(User user)
        {
            return user.Username;
        }

        public void setCoinsAmount(User user, int amount)
        {
            user.Coinsamount = amount;
        }
    }
}
