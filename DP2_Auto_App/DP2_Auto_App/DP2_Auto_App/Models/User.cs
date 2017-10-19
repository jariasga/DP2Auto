using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP2_Auto_App.Models
{
    public class User
    {
        private int idUser;
        private string username;
        private string password;
        public User() { }
        public User(string usr, string pass) {
            
            Username = parseNotNull(usr);
            Password = parseNotNull(pass);
        }

        private string parseNotNull(string text)
        {
            if (text != null) return text;
            else return "";
        }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
