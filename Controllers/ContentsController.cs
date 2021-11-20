using System;
using System.Net;
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
    public class ContentsController : RestController
    {
        private readonly QueryHelper _helper;
        private readonly OnlyartsContext _context;
        private readonly ILogger<UsersController> _logger;

        public ContentsController(ILogger<UsersController> logger, OnlyartsContext context, QueryHelper helper)
        {
            _helper = helper;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var contents = _helper.getMultipleByID<User>(id, new string[] {"User", "SubType"});
            if (contents.Count == 0) {
                return NotFound();
            }
            return Json(contents);
        }
        [HttpPost]
        public ActionResult Post(ContentRequest request)
        {
            var user = _helper.getByID<User>(request.UserID);
            var subType = _helper.getByID<SubType>(request.SubTypeID);
            if (user == null || subType == null) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var content = new Content
            {
                Name = request.Name,
                Description = request.Description,
                ContentType = request.ContentType,
                LinkToPreview = request.LinkToPreview,
                LinkToBlur = request.LinkToBlur,
                LikesCount = 0,
                DislikesCount = 0,
                ViewCount = 0,
                User = user,
                SubType = subType
            };
            _context.Contents.Add(content);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var content = _helper.getByID<Content>(id, new string[] {"User", "SubType"});
            if (content == null) {
                return NotFound();
            }
            return Json(content);
        }
        [HttpGet("{id}/likes")]
        public ActionResult GetLikes(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            return Json(content.LikesCount);
        }
        [HttpPatch("{id}/likes")]
        public ActionResult PatchLikes(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            content.LikesCount += 1;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPatch("{id}/dislikes")]
        public ActionResult PatchDislikes(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            content.DislikesCount += 1;
            _context.SaveChanges();
            return Ok();
        }
        [HttpPatch("{id}/view")]
        public ActionResult PatchViewCount(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            content.ViewCount += 1;
            _context.SaveChanges();
            return Ok();
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
            var content = GetContentsByTag(name);
            if (limit == 0) {
                return Json(content);
            }
            try {
                return Json(content.GetRange(0, limit));
            } 
            catch (ArgumentException) {
                return NotFound();
            }
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
