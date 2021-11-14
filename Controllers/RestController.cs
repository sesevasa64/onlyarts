using Microsoft.AspNetCore.Mvc;

[ApiController]
public class RestController : ControllerBase
{
    protected JsonResult Json(object o) 
    {
        return new JsonResult(o);
    }
}
