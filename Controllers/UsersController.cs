using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : RestController
    {
        private readonly OnlyartsContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, OnlyartsContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var users = (
                from user in _context.Users
                where id.Contains(user.Id)
                select user
            ).ToList();
            if (users.Count == 0) {
                return NotFound();
            }
            return Json(users);
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return ExampleJson(id);
        }
        [HttpPost("{id}")]
        public ActionResult Post(int id)
        {
            return ExampleJson(id);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return ExampleJson(id);
        }
        private ActionResult ExampleJson(int id) 
        {
            var users = (
                from user in _context.Users
                where user.Id == id
                select user
            ).ToList();
            if (users.Count == 0) {
                return NotFound();
            }
            return Json(users);
        }
    }
}
