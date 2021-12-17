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
    public class ReactionsController : RestController
    {
        private readonly string[] includes = new string[] {"User", "Content"};

        public ReactionsController(OnlyartsContext context, QueryHelper helper) :
            base(context, helper)
        {
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var reactions = _helper.getMultipleByID<Reaction>(id, includes);
            if (reactions.Count == 0) {
                return NotFound();
            }
            return Json(reactions);
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var reaction = _helper.getByID<Reaction>(id, includes);
            if (reaction == null) {
                return NotFound();
            }
            return Json(reaction);
        }
        [HttpGet("like")]
        public ActionResult CheckLike([FromQuery] int userId, [FromQuery] int contentId) 
        {
            return CheckReaction(contentId, userId, false);
        }
        [HttpGet("dislike")]
        public ActionResult CheckDislike([FromQuery] int userId, [FromQuery] int contentId) 
        {
            return CheckReaction(contentId, userId, true);
        }
        private ActionResult CheckReaction(int contentId, int userId, bool type) 
        {
            var content = _helper.getByID<Content>(contentId, ContentsController.includes);
            if (content == null) {
                return NotFound();
            }
            var user = _helper.getByID<User>(userId);
            if (user == null) {
                return NotFound();
            }
            var reaction = (
                from _reaction in _context.Reactions
                where _reaction.Content == content
                && _reaction.User == user
                && _reaction.Type == type
                select _reaction
            ).SingleOrDefault();
            if (reaction == null) {
                return NotFound();
            }
            return Ok();
        }
    }
}
