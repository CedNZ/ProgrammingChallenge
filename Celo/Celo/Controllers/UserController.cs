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
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult Get([FromQuery] int maxRecords = 20, [FromQuery] string nameSearch = "")
        {
            var allUsers = _userRepository.GetUsers();

            allUsers = allUsers.Where(u => u.Name.Contains(nameSearch, StringComparison.CurrentCultureIgnoreCase));

            return Ok(JsonSerializer.Serialize(allUsers.Take(maxRecords)));
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                return Ok(user.ToString());
            }
            return StatusCode(500, $"Unable to find User with Id {id}");
        }

        // POST: api/User/Update/5
        [HttpGet("Update/{id}", Name = "Update")]
        public ActionResult Update(int id)
        {
            var user = _userRepository.GetUserById(id);
            //var user = JsonSerializer.Deserialize<User>(userJson);
            return View(user);

        }

        // POST: api/User/Update/5
        [HttpPost("Update/{id}", Name = "Update")]
        public ActionResult Update(int id, [FromForm] User user)
        {
            //var user = JsonSerializer.Deserialize<User>(userJson);
            return _userRepository.UpdateUser(id, user) 
                ? Ok($"Succesfully updated User with Id {user.Id}")
                : StatusCode(500, $"Unable to update user with Id {user.Id}");

        }

        // DELETE: api/User/Delete/5
        [HttpPost("Delete/{id}", Name = "Delete")]
        public ActionResult Delete(int id)
        {
            return _userRepository.DeleteUser(id)
                ? Ok($"User with Id {id} successfully deleted")
                : StatusCode(500, $"Unable to delete user with Id {id}");
        }
    }
}
