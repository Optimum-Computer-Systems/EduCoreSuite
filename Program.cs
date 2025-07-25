using EducoreSuite.stmpservices;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using OfficeOpenXml;

// Configure EPPlus license context
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var builder = WebApplication.CreateBuilder(args);

// Register both DbContexts to support both features
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ForgeDBContext>(options =>
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
app.UseAuthorization();

// Map both MVC routes and Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapRazorPages();

app.Run();