using Microsoft.AspNetCore.Mvc;

namespace K21CNT2_2110900055_DATN.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
