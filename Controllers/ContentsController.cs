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
    [Route("api/contents")]
    public class ContentsController : RestController
    {
        private readonly OnlyartsContext _context;
        private readonly ILogger<UsersController> _logger;

        public ContentsController(ILogger<UsersController> logger, OnlyartsContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var contents = (
                from content in _context.Contents
                where id.Contains(content.Id)
                select content
            ).ToList();
            if (contents.Count == 0) {
                return NotFound();
            }
            return Json(contents);
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return ExampleJson(id);
        }
        [HttpGet("{id}/likes")]
        public ActionResult GetLikes(int id)
        {
            var likes = (
                from content in _context.Contents
                where content.Id == id
                select content.LikesCount
            ).ToList();
            return Json(likes);
        }
        [HttpGet("popular")]
        public ActionResult Get([FromQuery] int min, [FromQuery] int max)
        {
            // Задача для Артема Юнусова
            // Нужно вернуть популярный контент с min по max позиции
            return NotFound();
        }
        [HttpGet("tags/{name}")]
        public ActionResult Get(string name, [FromQuery] int limit)
        {
            // Задача для Артема Юнусова
            // Нужно вернуть популярный контент с min по max позиции
            return NotFound();
        }
        [HttpGet("users/{id}")]
        public ActionResult Get(int id, [FromQuery] int min, [FromQuery] int max)
        {
            var content = GetUserContent(id);
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
            var contents = (
                from content in _context.Contents
                where content.Id == id
                select content
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .ToList();
            if (contents.Count == 0) {
                return NotFound();
            }
            return Json(contents);
        }
        private List<Models.Content> GetUserContent(int id) 
        {
            var contents = (
                from content in _context.Contents
                join user in _context.Users
                on content.Id equals user.Id
                where user.Id == id
                select content
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .ToList();
            return contents;
        }
        // Метод, который возвращает контент с тегом name
        private List<Models.Content> GetContentsByTag(string name)
        {
            // Задача для Артема Юнусова
            // Раскоментировать и понять почему не работает join :)
            // ну и доделать по возможности
            /*
            var contents = (
                from linkTag in _context.LinkTags
                join tag in _context.Tags
                on linkTag.Tag equals tag.Id
            )
            */
            return null;
        }
    }
}
