using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class JsonDB
    {
        private readonly string _filename;

        public JsonDB(string filename)
        {
            this._filename = filename;
        }

        public bool AddUser(User user)
        {

            string json = JsonConvert.SerializeObject(user);
            using (StreamWriter sw = File.AppendText(_filename))
            {
                sw.WriteLine(json);
            }

            return false;
        }

        public User FindByUsername(string username)
        {
            string jsonUsers = File.ReadAllText(_filename);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonUsers);
            var user = users.FirstOrDefault(x => x.Username == username);
            return user;
        }
    }
}
