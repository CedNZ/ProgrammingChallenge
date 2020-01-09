using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Celo.Model;

namespace Celo.Repository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();

        public User GetUserById(int id);
    }
}
