using K21CNT2_2110900055_DATN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K21CNT2_2110900055_DATN.Controllers
{
    public class ProductController : Controller
    {
        private readonly K21cnt2PhanThiThuyLinh2110900055DatnContext _context;

        public ProductController(K21cnt2PhanThiThuyLinh2110900055DatnContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
