using Microsoft.AspNetCore.Mvc;

namespace NotesAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
