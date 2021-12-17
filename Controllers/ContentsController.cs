using System;
using System.Net;
using System.Linq;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;
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
        PropertyInfo[] content_props = typeof(Content).GetProperties();
        public static readonly string[] includes = new string[] {"User", "SubType"};
        public ContentsController(OnlyartsContext context, QueryHelper helper, FileUploader uploader) :
            base(context, helper)
        {
            _uploader = uploader;
        }
        protected override JsonResult Json(object o) 
        {
            List<Content> contents = o as List<Content>;
            if (contents == null) {
                contents = new List<Content>(){o as Content};
            }
            List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
            foreach (var content in contents) {
                IDictionary<string, object> x = new ExpandoObject();
                foreach (PropertyInfo prop in content.GetType().GetProperties()) {
                    x[prop.Name] = prop.GetValue(content, null);
                }
                x["LikesCount"] = _helper.GetLikesCount(content);
                x["DislikesCount"] = _helper.GetDislikesCount(content);
                result.Add(x);
            }
            return new JsonResult(result);
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var contents = _helper.getMultipleByID<User>(id, includes);
            if (contents.Count == 0) {
                return NotFound();
            }
            return Json(contents);
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
        [HttpPut]
        public ActionResult Put(ContentUpdateRequest request) 
        {
            var content = _helper.getByID<Content>(request.Id);
            if (content == null) {
                return NotFound();
            }
            content.Description = request.Description;
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var content = _helper.getByID<Content>(id, includes);
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
            return Json(_helper.GetLikesCount(content));
        }
        [HttpGet("{id}/dislikes")]
        public ActionResult GetDislikes(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            return Json(_helper.GetDislikesCount(content));
        }
        [HttpPatch("{id}/likes")]
        public ActionResult PatchLikes(int id, [FromQuery] int userID = -1)
        {
            if (userID == -1) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            var user = _helper.getByID<User>(userID);
            if (user == null) {
                return NotFound();
            }
            var reactions = GetUserReactionsOnContent(user, content);
            if (reactions != null) {
                return Conflict();
            }
            var reaction = CreateReaction(user, content, false);
            _context.Reactions.Add(reaction);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}/likes")]
        public ActionResult DeleteLikes(int id, [FromQuery] int userID = -1)
        {
            if (userID == -1) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            var user = _helper.getByID<User>(userID);
            if (user == null) {
                return NotFound();
            }
            var reaction = GetUserReactionsOnContent(user, content);
            if (reaction == null) {
                return Conflict();
            }
            if (reaction.Type == true) {
                return Conflict();
            }
            _context.Remove(reaction);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPatch("{id}/dislikes")]
        public ActionResult PatchDislikes(int id, [FromQuery] int userID = -1)
        {
            if (userID == -1) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            var user = _helper.getByID<User>(userID);
            if (user == null) {
                return NotFound();
            }
            var reactions = GetUserReactionsOnContent(user, content);
            if (reactions != null) {
                return Conflict();
            }
            var reaction = CreateReaction(user, content, true);
            _context.Reactions.Add(reaction);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}/dislikes")]
        public ActionResult DeleteDislikes(int id, [FromQuery] int userID = -1)
        {
            if (userID == -1) {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            var user = _helper.getByID<User>(userID);
            if (user == null) {
                return NotFound();
            }
            var reaction = GetUserReactionsOnContent(user, content);
            if (reaction == null) {
                return Conflict();
            }
            if (reaction.Type == false) {
                return Conflict();
            }
            _context.Remove(reaction);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}/view")]
        public ActionResult GetViewCount(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            return new JsonResult(content.ViewCount);
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
                select content
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .AsEnumerable()
            .OrderByDescending(contents => contents.ViewCount)
            .ThenBy(contents => _helper.GetLikesCount(contents))
            .ToList();
            if (min >= contents.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax<Content>(contents, min, max));
        }
        [HttpGet("popular/{name}")]
        public ActionResult Get(string name, [FromQuery] int min, [FromQuery] int max)
        {
            var contents = (
                from content in _context.Contents
                join tag in _context.LinkTags
                on content equals tag.Content
                where tag.Tag.TagName == name
                select content
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .AsEnumerable()
            .OrderByDescending(contents => contents.ViewCount)
            .ThenBy(contents => _helper.GetLikesCount(contents))
            .ToList();
            if (min >= contents.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax<Content>(contents, min, max));
        }
        [HttpGet("popular/user/{login}")]
        public ActionResult GetContentByLogin(string login, [FromQuery] int min, [FromQuery] int max)
        {
            var contents = (
                from content in _context.Contents
                join user in _context.Users
                on content.User equals user
                where user.Login == login
                select content
            ).Include(content => content.User)
            .Include(contents => contents.SubType)
            .AsEnumerable()
            .OrderByDescending(contents => contents.ViewCount)
            .ThenBy(contents => _helper.GetLikesCount(contents))
            .ToList();
            if (min >= contents.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax<Content>(contents, min, max));
        }
        [HttpGet("tags/{name}")]
        public ActionResult Get(string name, [FromQuery] int limit)
        {
            var content = GetContentsByTag(name);
            if (limit == 0 || limit >= content.Count) {
                return Json(content);
            }
            return Json(content.GetRange(0, limit));
        }
        [HttpGet("tag/{name}")]
        public ActionResult GetContents(string name, [FromQuery] int min, [FromQuery] int max)
        {
            var content = (
                from contents in _context.Contents
                join linkTag in _context.LinkTags 
                on contents equals linkTag.Content
                where linkTag.Tag.TagName == name
                select contents
            ).Include(contents => contents.User)
            .Include(contents => contents.SubType)
            .AsEnumerable()
            .OrderByDescending(contents => contents.ViewCount)
            .ThenBy(contents => _helper.GetLikesCount(contents))
            .ToList();
            if (min >= content.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax(content, min, max));
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
            if (min >= content.Count) {
                return NotFound();
            }
            return Json(_helper.GetMinMax<Content>(content, min, max));
        }
        [HttpGet("like")]
        public ActionResult GetContentByName([FromQuery] string name)
        {
            var contents = (
                from content in _context.Contents
                where content.Name.Contains(name)
                select content
            ).Include(content => content.User)
            .Include(content => content.SubType)
            .ToList();
            if (contents == null) {
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
        private Reaction GetUserReactionsOnContent(User user, Content content)
        {
           var reactions = (
                from _reaction in _context.Reactions
                where _reaction.Content == content && _reaction.User == user
                select _reaction
            ).SingleOrDefault(); 
            return reactions;
        }
        private Reaction CreateReaction(User user, Content content, bool Type) 
        {
            var reaction = new Reaction {
                Type = Type,
                User = user,
                Content = content
            };
            return reaction;
        }
    }
}
