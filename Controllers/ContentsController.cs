using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlyarts.Data;

namespace onlyarts.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            ).ToList();
            if (contents.Count == 0) {
                return NotFound();
            }
            return Json(contents);
        }
    }
}
