using K21CNT2_2110900055_DATN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using static K21CNT2_2110900055_DATN.ViewModel.AccountViewModel;
using Microsoft.AspNetCore.Http;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace K21CNT2_2110900055_DATN.Controllers
{
    public class AccountController : Controller
    {
        private readonly K21cnt2PhanThiThuyLinh2110900055DatnContext _context;
        public INotyfService _notifyService { get; }
        public AccountController(K21cnt2PhanThiThuyLinh2110900055DatnContext context, INotyfService notifyService)
            {
                _context = context;
                _notifyService = notifyService;
        }

        public IActionResult Register() => View();
        public IActionResult MyAccount() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Accounts.FirstOrDefault(a => a.Email == model.Email);
                if (existingUser != null)
                {
                    ViewBag.Error = "Email đã được sử dụng!";
                    return View(model);
                }

                var newUser = new Account
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = model.Password, // Lưu mật khẩu dưới dạng plaintext
                    RoleId = 2, // Mặc định là User
                    CreateDate = DateTime.Now
                };

                _context.Accounts.Add(newUser);
                _notifyService.Success("Đăng kí tài khoản thành công");
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var account = _context.Accounts.FirstOrDefault(a => a.Email == model.Email);

            if (account != null && account.Password == model.Password) // Không mã hóa nữa
            {
                HttpContext.Session.SetInt32("UserId", account.AccountId);
                HttpContext.Session.SetInt32("RoleId", account.RoleId); // Lưu Role vào Session
                account.LastLogin = DateTime.Now;
                _notifyService.Success("Đăng nhập tài khoản thành công");
                _context.SaveChanges();

                // Kiểm tra nếu RoleId = 1 thì chuyển đến Admin Dashboard
                if (account.RoleId == 1)
                {
                    return RedirectToAction("Index", "Admin"); // Điều hướng sang trang Admin
                }

                return RedirectToAction("Index", "Home"); // Điều hướng User thông thường
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng!";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }    
}
