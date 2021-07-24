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

        public void AddUser(User user)
        {
            List<User> users = new List<User>()
            {
                user,

            };
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);

            if (!File.Exists(_filename))
                File.AppendAllText(_filename, json);
            else
            {
                
                File.AppendAllText(_filename, json);
                string text = File.ReadAllText(_filename);
                text = text.Replace("][", ",");
                File.WriteAllText(_filename, text);
                
            }

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
