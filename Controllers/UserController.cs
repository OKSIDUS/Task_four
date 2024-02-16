using Microsoft.AspNetCore.Mvc;

namespace Task_four.Controllers
{
    public class UserController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
    }
}
