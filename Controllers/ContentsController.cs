using System;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly FileUploader _uploader;
        private readonly QueryHelper _helper;
        private readonly OnlyartsContext _context;
        private readonly ILogger<UsersController> _logger;

        public ContentsController(ILogger<UsersController> logger, OnlyartsContext context, QueryHelper helper, FileUploader uploader)
        {
            _helper = helper;
            _logger = logger;
            _context = context;
            _uploader = uploader;
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
        public static Dictionary<String, Object> parse(byte[] json){
            string jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(jsonStr);
        }
        [HttpPost]
        public ActionResult Post(ContentRequest request)
        {
            if (request.Images.Count == 0) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
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
            
            var returnCodes = new List<int>();
            for (int i = 0; i < request.Images.Count; i++) {
                var result = _uploader.UploadFile(request.Images[i]);
                if (result.ContainsKey("error")) {
                    returnCodes.Add(i);
                    continue;
                }
                var data = (result["data"] as JObject).ToObject<Dictionary<string, object>>();
                var url = data["url"].ToString();
                var image = new Image {
                    Content = content,
                    LinkToImage = url
                };
                _context.Images.Add(image);
            }
            if (returnCodes.Count == request.Images.Count) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }

            _context.Contents.Add(content);
            _context.SaveChanges();

            if (returnCodes.Count != 0) {
                return UnprocessableEntity(Json(returnCodes));
            }
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
            var contents = (
                from content in _context.Contents
                orderby content.ViewCount, content.LikesCount descending
                select content
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .ToList();
            if (min == 0 && max == 0) {
                return Json(contents);
            }
            try {
                return Json(contents.GetRange(min, max - min));
            } 
            catch (ArgumentException) {
                return NotFound();
            }
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
        [HttpGet("users")]
        public ActionResult Get([FromQuery] string login)
        {
            var user = _helper.getUserByLogin(login);
            if (user == null) {
                return NotFound();
            }
            var content = (
                from _content in _context.Contents
                where _content.User == user
                select _content
            ).Include(content => content.User)
            .Include(content => content.SubType)
            .ToList();
            if (content.Count == 0) {
                return NotFound();
            }
            return Json(content);
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
            var contents = (
                from content in _context.Contents
                join linkTag in _context.LinkTags 
                on content equals linkTag.Content
                where linkTag.Tag.TagName == name
                select content 
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .ToList();
            return contents;
        }
    }
}
