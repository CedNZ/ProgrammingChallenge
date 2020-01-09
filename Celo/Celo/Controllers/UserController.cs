using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Celo.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using Celo.Repository;

namespace Celo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public string Get([FromQuery] int maxRecords = 20, [FromQuery] string nameSearch = "")
        {
            var allUsers = _userRepository.GetUsers();

            allUsers = allUsers.Where(u => u.Name.Contains(nameSearch));

            return JsonSerializer.Serialize(allUsers.Take(maxRecords));
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return _userRepository.GetUserById(id).ToString();
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
