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
    public class AccessController : RestController
    {
        public AccessController(OnlyartsContext context, QueryHelper helper, FileUploader uploader) :
            base(context, helper)
        {
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int userId, [FromQuery] int contentId)
        {
            var subUser = _helper.getByID<User>(userId);
            if (subUser == null) {
                return NotFound();
            }
            var content = _helper.getByID<Content>(contentId, ContentsController.includes);
            if (content == null) {
                return NotFound();
            }
            var author = content.User;
            var sub = (
                from _sub in _context.Subscriptions
                where _sub.Author == author && _sub.SubUser == subUser
                select _sub
            // Каждый пользователь может подписаться только один раз на другого пользователя
            ).SingleOrDefault();
            if (sub == null) {
                return NotFound();
            }
            return Json(content.SubType.SubLevel <= sub.SubType.SubLevel);
        }
    }
}
