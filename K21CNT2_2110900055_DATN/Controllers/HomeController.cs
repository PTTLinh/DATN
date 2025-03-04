using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using K21CNT2_2110900055_DATN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Outlook;

namespace K21CNT2_2110900055_DATN.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly K21cnt2PhanThiThuyLinh2110900055DatnContext _context;

    public HomeController(ILogger<HomeController> logger, K21cnt2PhanThiThuyLinh2110900055DatnContext context)
    {
        _logger = logger;
        _context = context;
    }


    public IActionResult Index()
    {
        var products = _context.Products
            .AsNoTracking()
            .Take(8) // Lấy 8 sản phẩm trong danh sách
            .ToList();

        // Lấy 3 bài blog mới nhất
        var blogs = _context.Blogs
            .AsNoTracking()
            .OrderByDescending(x => x.BlogId) 
            .Take(2)
            .ToList();

        // Gửi danh sách sản phẩm ra View
        ViewBag.Products = products;
        // Đẩy dữ liệu blog ra View
        ViewBag.Blogs = blogs; 
        return View();
    }


    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
