using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<string> Users;
    }
}
