using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Service
{
    public class DatabaseUserProvider : IUserProvider
    {
        public void addNewUser(string username, string email, string password)
        {
            User newUser = new User();
            using (var context = new TTContext())
            {
                newUser.Username = username;
                newUser.Email = email;
                newUser.Password = password;
                newUser.Passwordsalt = BCrypt.Net.BCrypt.GenerateSalt();
                context.Users.Add(newUser);
                context.SaveChanges();
            }
        }
        public User getUserByEmail(string email)
        {
            User user = new User();
            using (var context = new TTContext())
            {
                var allUsers = context.Users.ToList();
                foreach (User u in allUsers)
                {
                    if (getEmail(u) == email)
                    {
                        user = u;
                    }
                }
            }
            return user;
        }
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
