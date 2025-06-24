using EduCoreSuite.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register MVC controller services
builder.Services.AddControllers(); // <-- Add this line
builder.Services.AddRazorPages();

// ✅ Register your DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ✅ Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ✅ Map Razor Pages and API Controllers
app.MapRazorPages();
app.MapControllers(); // <-- Add this line to expose your API endpoints

app.Run();
