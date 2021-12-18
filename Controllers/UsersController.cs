using System;
using System.Net;
using System.Linq;
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
        private readonly TokenGenerator _tokenGenerator;
        public UsersController(OnlyartsContext context, TokenGenerator tokenGenerator, QueryHelper helper) :
            base(context, helper)
        {
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
            var user = _helper.getUserByLogin(login);
            if (user == null) {
                return NotFound();
            }
            return Json(user);
        }
        [HttpPost("subscribe")]
        public ActionResult Post(UserSubscribeRequest request)
        {
            var author = _helper.getByID<User>(request.AuthorId);
            var user = _helper.getByID<User>(request.SubUserId);
            if (author == null || user == null) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var subtype = (
                from subtypes in _context.SubTypes
                where subtypes.Id == request.SubTypeId
                select subtypes 
            ).SingleOrDefault();
            var sub = new Subscription 
            {
                EndSubDate = DateTime.Now,
                SubUser = user,
                Author = author,
                SubType = subtype
            };
            _context.Subscriptions.Add(sub);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("unsubscribe")]
        public ActionResult DeleteSubscriber([FromQuery] int authorID, [FromQuery] int subuserID)
        {
            if (authorID == -1 || subuserID == -1) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var sub = GetSubByUsersId(authorID, subuserID);
            if (sub == null) {
                return NotFound();
            }
            _context.Remove(sub);
            _context.SaveChanges();
            return Ok();
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
        [HttpPut]
        public ActionResult Put(UserUpdateRequest request) 
        {
            var user = _helper.getByID<User>(request.Id);
            if (user == null) {
                return NotFound();
            }
            user.Nickname = request.Nickname;
            user.Info = request.Info;
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
        [HttpGet("popular")]
        public ActionResult Get([FromQuery] int min, [FromQuery] int max)
        {
            var users = GetPopularUsers();
            if (min >= users.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax<User>(users, min, max));
        }
        [HttpGet("subscribers/{login}")]
        public ActionResult GetSubs(string login, [FromQuery] int min, [FromQuery] int max) 
        {
            var subs = GetSubscribers(login);
            if (min >= subs.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax(subs, min, max));
        }
        public List<User> GetSubscribers(string login)
        {
            var subs = (from user in _context.Users
                        join sub in _context.Subscriptions
                        on user equals sub.SubUser
                        where sub.Author.Login == login
                        select sub.SubUser).ToList();
            return subs;
        }
        private List<User> GetPopularUsers()
        {
            // Задача для Артема Юнусова
            // Нужно вернуть список популярных юзеров с min по max позиции
            var subsCount = (
                from sub in _context.Subscriptions.Include(x => x.Author).Include(x => x.SubUser).AsEnumerable()
                group sub by sub.Author into subGB
                select new {User = subGB.Key, SubUser = from s in subGB select s.SubUser, Count = (from s in subGB select s.SubUser).Count()}
            );
            var users = (
                from user in subsCount
                orderby user.Count descending
                select user.User
            ).ToList();
            return users;
        }
        private Subscription GetSubByUsersId(int AuthorID, int SubUserID) 
        {
            var subs = (
                from sub in _context.Subscriptions
                where sub.Author.Id == AuthorID && sub.SubUser.Id == SubUserID 
                select sub
            ).SingleOrDefault();
            return subs;
        }
    }
}
