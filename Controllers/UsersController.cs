using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using onlyarts.Services;
using onlyarts.Models;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : RestController
    {
        private readonly OnlyartsContext _context;
        private readonly TokenGenerator _tokenGenerator;
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger, OnlyartsContext context, TokenGenerator tokenGenerator)
        {
            _logger = logger;
            _context = context;
            _tokenGenerator = tokenGenerator;
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
        [HttpGet("popular")]
        public ActionResult Get([FromQuery] int min, [FromQuery] int max)
        {
            // Задача для Артема Юнусова
            // Нужно вернуть список популярных юзеров с min по max позиции
            return NotFound();
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
        [HttpPost("auth")]
        public ActionResult Auth(AuthenticationRequest request) 
        {
            var user = (
                from _user in _context.Users
                where _user.Login == request.Login && 
                      _user.Password == request.Password
                select _user
            ).SingleOrDefault();
            if (user == null) {
                // Должно быть 401 а не 404
                return NotFound();
            }
            return Json(new AuthenticationResponse {
                AuthToken = _tokenGenerator.generateAuthToken(user)
            });
        }
    }
}
