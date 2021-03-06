﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Celo.Model;
using System.Text.Json;

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
        public ActionResult Index([FromQuery] int maxRecords = 20, [FromQuery] string nameSearch = "", [FromQuery] bool asJson = false)
        {
            nameSearch ??= "";

            var allUsers = _userRepository.GetUsers();

            allUsers = allUsers.Where(u => u.Name.Contains(nameSearch, StringComparison.CurrentCultureIgnoreCase));

            if(asJson)
            {
                return new JsonResult(allUsers.Take(maxRecords).ToList());
            }

            ViewBag.MaxRecords = maxRecords;
            ViewBag.NameSearch = nameSearch;

            return View(allUsers.Take(maxRecords));
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


        // GET: api/User/ViewUser/5
        [HttpGet("ViewUser/{id}", Name = "ViewUser")]
        public ActionResult ViewUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if(user != null)
            {
                return View(user);
            }
            return StatusCode(404);
        }

        // POST: api/User/Update/5
        [HttpGet("Update/{id}", Name = "Update")]
        public ActionResult Update(int id)
        {
            var user = _userRepository.GetUserById(id);
            return View(user);
        }

        // POST: api/User/Update/5
        [HttpPost("Update/{id}", Name = "Update")]
        public ActionResult Update(int id, [FromForm] User user)
        {
            if(_userRepository.UpdateUser(id, user))
            {
                return RedirectToAction("ViewUser", new { id });
            }
            return StatusCode(500, $"Unable to update user with Id {user.Id}");

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
