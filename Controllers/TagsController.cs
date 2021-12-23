using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using onlyarts.Services;
using onlyarts.Models;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : RestController
    {
        public TagsController(OnlyartsContext context, QueryHelper helper) :
            base(context, helper)
        {
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Роут для получения списка тегов по их id")]
        public ActionResult GetMultipleById([FromQuery] int[] id)
        {
            var tags = _helper.getMultipleByID<Tag>(id);
            if (tags.Count == 0) {
                return NotFound();
            }
            return Json(tags);
        }
        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Роут для получения тега по id")]
        public ActionResult GetById(int id)
        {
            var tag = _helper.getByID<Tag>(id);
            if (tag == null) {
                return NotFound();
            }
            return Json(tag);
        }
        [HttpGet("popular")]
        [SwaggerOperation(Summary = "Роут для получения популярных тегов")]
        public ActionResult GetPopularTags([FromQuery] int min, [FromQuery] int max)
        {
            // Подсчёт количества встречающихся тегов
            // На выходе {Tag ; Количество упоминаний тега в контентах}
            var tagsCount = (
                from linkTag in _context.LinkTags.Include(x => x.Tag).AsEnumerable()
                group linkTag by linkTag.Tag into linkTagGB
                select new {Tag = linkTagGB.Key, Count = linkTagGB.Count()});
            var tags = (
                from tag in tagsCount
                orderby tag.Count descending
                select tag.Tag
            ).Distinct().ToList();
            if (min == 0 & max == 0) {
                return Json(tags);
            }
            if (tags.Count < max)
            {
                return Json(tags.GetRange(min, tags.Count - min));
            }
            try {
                return Json(tags.GetRange(min, max - min));
            }
            catch (ArgumentException) {
                return NotFound();
            }
            // Задача для Артема Юнусова
            // Нужно вернуть популярные теги с min по max позиции
        }
    }
}
