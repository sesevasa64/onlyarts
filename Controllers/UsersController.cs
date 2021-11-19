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
        private readonly QueryHelper _helper;
        private readonly OnlyartsContext _context;
        private readonly TokenGenerator _tokenGenerator;
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger, OnlyartsContext context, TokenGenerator tokenGenerator, QueryHelper helper)
        {
            _helper = helper;
            _logger = logger;
            _context = context;
            _tokenGenerator = tokenGenerator;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id, [FromQuery] string login)
        {
            bool isIdEmpty = id.Length == 0;
            bool isLoginNull = login == null;
            if (!(isIdEmpty ^ isLoginNull)) {
                return BadRequest();
            }
            if (!isIdEmpty) {
                var users = _helper.getMultipleByID<User>(id);
                if (users.Count == 0) {
                    return NotFound();
                }
                return Json(users);
            }
            var user = (
                from _user in _context.Users
                where _user.Login == login
                select _user
            ).SingleOrDefault();
            if (user == null) {
                return NotFound();
            }
            return Json(user);
        }
        [HttpPost]
        public ActionResult Post(RegistrationRequest request)
        {
            var check = (
                from _user in _context.Users
                where _user.Login == request.Login || _user.Email == request.Email
                select _user
            ).ToList();
            if (check.Count != 0) {
                return Conflict();
            }
            var user = new User 
            {
                Login = request.Login,
                Password = request.Password,
                Nickname = request.Nickname,
                Email = request.Email,
                LinkToAvatar = request.LinkToAvatar,
                RegisDate = DateTime.Now,
                Money = 0
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var user = _helper.getByID<User>(id);
            if (user == null) {
                return NotFound();
            }
            return Json(user);
        }
        [HttpGet("popular")]
        public ActionResult Get([FromQuery] int min, [FromQuery] int max)
        {
            // Задача для Артема Юнусова
            // Нужно вернуть список популярных юзеров с min по max позиции
            return NotFound();
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
                return Unauthorized();
            }
            return Json(new AuthenticationResponse {
                AuthToken = _tokenGenerator.generateAuthToken(user)
            });
        }
    }
}
