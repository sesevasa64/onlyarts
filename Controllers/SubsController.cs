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
    public class SubsController : RestController
    {
        private readonly string[] includes = new string[] {"Author", "SubUser", "SubType"};
        public SubsController(OnlyartsContext context, QueryHelper helper) :
            base(context, helper)
        {
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var subs = _helper.getMultipleByID<Subscription>(id, includes);
            if (subs.Count == 0) {
                return NotFound();
            }
            return Json(subs);
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var subs = _helper.getByID<Subscription>(id, includes);
            if (subs == null) {
                return NotFound();
            }
            return Json(subs);
        }
        [HttpGet("users/{id}")]
        public ActionResult Get(int id, [FromQuery] int min, [FromQuery] int max)
        {
            var subs = GetUserSubs(id);
            if (min >= subs.Count) {
                return NotFound();
            }
            return Json((
                from user in _helper.GetMinMax<User>(subs, min, max)
                select new UserSubsResponse(user.Login, user.Nickname, user.LinkToAvatar)
            ).ToList());
        }
        [HttpPost("{id}")]
        public ActionResult Post(int id)
        {
            return Get(id);
        }
        // Метод, который возвращает подписчиков юзера с идентификатором id
        private List<Models.User> GetUserSubs(int id) 
        {
            var subscribers = (
                from subscriber in _context.Subscriptions
                where subscriber.Author.Id == id
                select subscriber.SubUser
            ).ToList();
            return subscribers;
        }
    }
}
