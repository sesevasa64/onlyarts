using Microsoft.AspNetCore.Mvc;

public class RestController : ControllerBase
{
    protected JsonResult Json(object o) 
    {
        return new JsonResult(o);
    }
}
