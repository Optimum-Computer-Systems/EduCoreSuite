
﻿using EduCoreSuite.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
﻿using EducoreSuite.stmpservices;

using EducoreSuite.stmpservices;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

// Configure EPPlus license context
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register MVC controller services
builder.Services.AddControllers(); // <-- Add controllers
builder.Services.AddRazorPages();

// ✅ Register your DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();


// Register both DbContexts to support both features
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// Add MVC services and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register application services
builder.Services.AddScoped<EduCoreSuite.Services.ActivityService>();
builder.Services.AddScoped<EduCoreSuite.Services.ExportService>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

var app = builder.Build();

// Configure request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.MapFallbackToController("Login", "Auth");

// Map both MVC routes and Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
);
app.MapRazorPages();


app.Run();

//var app = builder.Build();

//// ✅ Configure HTTP request pipeline
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//// ✅ Map Razor Pages and API Controllers
//app.MapRazorPages();
//app.MapControllers(); // <-- Add this line to expose your API endpoints

//app.Run();

app.Run();

