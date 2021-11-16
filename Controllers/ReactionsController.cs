using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionsController : RestController
    {
        private readonly OnlyartsContext _context;
        private readonly ILogger<UsersController> _logger;

        public ReactionsController(ILogger<UsersController> logger, OnlyartsContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var reactions = (
                from reaction in _context.Reactions
                where id.Contains(reaction.Id)
                select reaction
            ).ToList();
            if (reactions.Count == 0) {
                return NotFound();
            }
            return Json(reactions);
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
            var reactions = (
                from reaction in _context.Reactions
                where reaction.Id == id
                select reaction
            ).Include(reactions => reactions.User)
            .Include(reactions => reactions.Content)
            .ToList();
            if (reactions.Count == 0) {
                return NotFound();
            }
            return Json(reactions);
        }
    }
}
