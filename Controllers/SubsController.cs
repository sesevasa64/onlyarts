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
    [Route("api/subscriptions")]
    public class SubsController : RestController
    {
        private readonly OnlyartsContext _context;
        private readonly ILogger<SubsController> _logger;
        public SubsController(ILogger<SubsController> logger, OnlyartsContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var subs = (
                from sub in _context.Subscriptions
                where id.Contains(sub.Id)
                select sub
            ).Include(subs => subs.Author)
            .Include(subs => subs.SubUser)
            .Include(subs => subs.SubType)
            .ToList();
            if (subs.Count == 0) {
                return NotFound();
            }
            return Json(subs);
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return ExampleJson(id);
        }
        [HttpGet("users/{id}")]
        public ActionResult Get(int id, [FromQuery] int min, [FromQuery] int max)
        {
            var content = GetUserSubs(id);
            if (min == 0 & max == 0) {
                return Json(content);
            }
            try {
                return Json(content.GetRange(min, max - min));
            } 
            catch (ArgumentException) {
                return NotFound();
            }
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
            var subs = (
                from sub in _context.Subscriptions
                where sub.Id == id
                select sub
            ).Include(subs => subs.Author)
            .Include(subs => subs.SubUser)
            .Include(subs => subs.SubType)
            .ToList();
            if (subs.Count == 0) {
                return NotFound();
            }
            return Json(subs);
        }
        // Метод, который возвращает подписчиков юзера с идентификатором id
        private List<Models.User> GetUserSubs(int id) 
        {
            // Задача для Артема Юнусова
            return null;
        }
    }
}
