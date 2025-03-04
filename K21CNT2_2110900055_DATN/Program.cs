using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using K21CNT2_2110900055_DATN.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;


var builder = WebApplication.CreateBuilder(args);


// Cấu hình dịch vụ DbContext
builder.Services.AddDbContext<K21cnt2PhanThiThuyLinh2110900055DatnContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbWebThoiTrangAdidas")));

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Cấu hình HtmlEncoder với các phạm vi Unicode cho phép
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

// Thêm session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session tồn tại
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDistributedMemoryCache(); // Cần thiết để sử dụng session

//Sử dụng ToastNotification để thông báo
builder.Services.AddNotyf(options =>
{
    options.DurationInSeconds = 10;
    options.IsDismissable = true;
    options.Position = NotyfPosition.TopRight;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession(); // Kích hoạt session
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.UseNotyf();

// Định tuyến cho Areas (Admin, User, ...)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Định tuyến mặc định cho các Controller không thuộc Area
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();
