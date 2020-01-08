using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Celo.Model;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Celo.Repository
{
    public class UserRepository
    {
        private LiteDatabase database;

        public UserRepository()
        {
            database = new LiteDatabase("Users.db");
            if (!database.CollectionExists("Users"))
            {
                var usersCollection = database.GetCollection<User>("Users");
                var users = System.Text.Json.JsonSerializer.Deserialize<User[]>(System.IO.File.ReadAllText("Users.json"));
                //usersCollection.InsertBulk(users);

                foreach(var user in users)
                {
                    //LiteDB is throwing Null Reference Exceptions. No idea why, but swallowing the exception works
                    try
                    {
                        usersCollection.Insert(user);
                    } catch { }
                }
            }
        }

        public LiteCollection<User> Users => database.GetCollection<User>("Users");
    }
}
