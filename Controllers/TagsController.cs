using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : RestController
    {
        private readonly OnlyartsContext _context;
        private readonly ILogger<TagsController> _logger;

        public TagsController(ILogger<TagsController> logger, OnlyartsContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var tags = (
                from tag in _context.Tags
                where id.Contains(tag.Id)
                select tag
            ).ToList();
            if (tags.Count == 0) {
                return NotFound();
            }
            return Json(tags);
        }
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return ExampleJson(id);
        }
        [HttpGet("popular")]
        public ActionResult Get([FromQuery] int min, [FromQuery] int max)
        {
            if (min == 0 & max == 0) {
                return NotFound();
            }
            // Задача для Артема Юнусова
            // Нужно вернуть популярные теги с min по max позиции
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
            var tags = (
                from tag in _context.Tags
                where tag.Id == id
                select tag
            ).ToList();
            if (tags.Count == 0) {
                return NotFound();
            }
            return Json(tags);
        }
    }
}
