using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlyarts.Controllers;
using onlyarts.Services;
using onlyarts.Data;

[ApiController]
public class RestController : ControllerBase
{
    protected QueryHelper _helper;
    protected OnlyartsContext _context;
    public RestController(OnlyartsContext context, QueryHelper helper)
    {
        _helper = helper;
        _context = context;
    }
    virtual protected JsonResult Json(object o) 
    {
        return new JsonResult(o);
    }
}
