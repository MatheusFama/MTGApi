using Microsoft.AspNetCore.Mvc;
using MTGApi.Entities;

namespace MTGApi.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Account? Account => HttpContext.Items["Account"] as Account; 
    }
}
