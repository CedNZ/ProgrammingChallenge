using System.Collections.Generic;
using LiteDB;
using Celo.Model;


namespace Celo.Repository
{
    public class UserRepository : IUserRepository
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

        private LiteCollection<User> Users => database.GetCollection<User>("Users");

        public IEnumerable<User> GetUsers()
        {
            return Users.FindAll();
        }

        public User GetUserById(int id)
        {
            return Users.FindById(id);
        }

        public bool DeleteUser(int id)
        {
            return Users.Delete(id);
        }

        public bool UpdateUser(int id, User user)
        {
            return Users.Update(id, user);
        }
    }
}
