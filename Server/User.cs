using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        public User()
        {

        }
        public User(string Fullname, string Username, string Password)
        {
            this.Fullname = Fullname;
            this.Username = Username;
            this.Password = Password;
        }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

       
    }
}
