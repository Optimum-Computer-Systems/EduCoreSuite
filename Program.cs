using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;

var builder = WebApplication.CreateBuilder(args);

// Register the ForgeDBContext with SQL Server and enable retry on failure
builder.Services.AddDbContext<ForgeDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// Add MVC services
builder.Services.AddControllersWithViews();

// Register application services
builder.Services.AddScoped<EduCoreSuite.Services.ActivityService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
