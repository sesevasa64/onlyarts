using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Роут для получения списка реакций по их id")]
        public ActionResult GetMultipleById([FromQuery] int[] id)
        {
            var reactions = _helper.getMultipleByID<Reaction>(id, includes);
            if (reactions.Count == 0) {
                return NotFound();
            }
            return Json(reactions);
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Роут для получения реакции по id")]
        public ActionResult GetById(int id)
        {
            var reaction = _helper.getByID<Reaction>(id, includes);
            if (reaction == null) {
                return NotFound();
            }
            return Json(reaction);
        }
        [HttpGet("like")]
        [SwaggerOperation(Summary = "Роут для проверки наличия лайка юзера userId на контенте contentId")]
        public ActionResult CheckLike([FromQuery] int userId, [FromQuery] int contentId) 
        {
            return CheckReaction(contentId, userId, false);
        }
        [HttpGet("dislike")]
        [SwaggerOperation(Summary = "Роут для проверки наличия дизлайка юзера userId на контенте contentId")]
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
