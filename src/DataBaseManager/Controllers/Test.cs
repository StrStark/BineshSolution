using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Test : Controller
    {
        [HttpGet]
        public ActionResult FillMockData()
        {
            return Ok();
        }
    }
}
