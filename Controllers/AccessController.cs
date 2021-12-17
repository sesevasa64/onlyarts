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
    class AccessController : RestController
    {
        public AccessController(OnlyartsContext context, QueryHelper helper, FileUploader uploader) :
            base(context, helper)
        {
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
