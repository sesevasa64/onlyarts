using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using onlyarts.Services;
using onlyarts.Models;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : RestController
    {
        private readonly QueryHelper _helper;
        private readonly OnlyartsContext _context;
        private readonly ILogger<TagsController> _logger;

        public TagsController(ILogger<TagsController> logger, OnlyartsContext context, QueryHelper helper)
        {
            _helper = helper;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var tags = _helper.getMultipleByID<Tag>(id);
            if (tags.Count == 0) {
                return NotFound();
            }
            return Json(tags);
        }
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
         var tag = _helper.getByID<Tag>(id);
            if (tag == null) {
                return NotFound();
            }
            return Json(tag);
        }
        [HttpPost("{id}")]
        public ActionResult Post(int id)
        {
            return Get(id);
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
    }
}
