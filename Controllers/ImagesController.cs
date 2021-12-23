using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using onlyarts.Services;
using onlyarts.Models;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : RestController
    {
        private readonly string[] includes = new string[] {"Content"};
        public ImagesController(OnlyartsContext context, QueryHelper helper) :
            base(context, helper)
        {
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Роут для получения списка изображений по их id")]
        public ActionResult GetMultipleById([FromQuery] int[] id)
        {
            var images = _helper.getMultipleByID<Image>(id, includes);
            if (images.Count == 0) {
                return NotFound();
            }
            return Json(images);
        }
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Роут для получения изображения по id")]
        public ActionResult GetById(int id)
        {
            var images = _helper.getByID<Image>(id, includes);
            if (images == null) {
                return NotFound();
            }
            return Json(images);
        }
        [HttpGet("contents/{id}")]
        [SwaggerOperation(Summary = "Роут для получения изображений контента по id")]
        public ActionResult GetByContentId(int id)
        {
            var content = _helper.getByID<Content>(id);
            if (content == null) {
                return NotFound();
            }
            var images = (
                from image in _context.Images
                where image.Content == content
                select image.LinkToImage
            ).ToList();
            if (images.Count == 0) {
                return NotFound();
            }
            return Json(images);
        }
    }
}
