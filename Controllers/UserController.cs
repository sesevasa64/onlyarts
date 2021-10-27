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
    [Route("[controller]/{id:int=-1}")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, UserContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get(int id)
        {
            if (IsDefaultId(id)) {
                Console.WriteLine("Default id!");
            }
            else {
                Console.WriteLine(id);
            }
            return ExampleJson();
        }
        [HttpPost]
        public ActionResult Post(int id)
        {
            if (IsDefaultId(id)) {
                Console.WriteLine("Default id!");
            }
            else {
                Console.WriteLine(id);
            }
            return ExampleJson();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (IsDefaultId(id)) {
                Console.WriteLine("Default id!");
            }
            else {
                Console.WriteLine(id);
            }
            return ExampleJson();
        }
        private JsonResult ExampleJson() 
        {
            var users = (
                from user in _context.Users
                where user.Id == 1
                select user
            ).ToList();
            return new JsonResult(users);
        }
        private bool IsDefaultId(int id) 
        {
            return id == -1;
        }
    }
}
