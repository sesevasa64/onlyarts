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
        [HttpPost("{id}")]
        public ActionResult Post(int id)
        {
            return Get(id);
        }
    }
}
