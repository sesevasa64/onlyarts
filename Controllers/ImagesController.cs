using System;
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
    public class ImagesController : RestController
    {
        private readonly QueryHelper _helper;
        private readonly OnlyartsContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly string[] includes = new string[] {"Content"};
        public ImagesController(ILogger<UsersController> logger, OnlyartsContext context, QueryHelper helper)
        {
            _helper = helper;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult Get([FromQuery] int[] id)
        {
            var images = _helper.getMultipleByID<Image>(id, includes);
            if (images.Count == 0) {
                return NotFound();
            }
            return Json(images);
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var images = _helper.getByID<Image>(id, includes);
            if (images == null) {
                return NotFound();
            }
            return Json(images);
        }
        [HttpPost("{id}")]
        public ActionResult Post(int id)
        {
            return Get(id);
        }
        [HttpGet("contents/{id}")]
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
