using AssignmentAsp.Net.Data;
using AssignmentAsp.Net.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES TO DI CONTAINER
// 1. cotroller with views
builder.Services.AddControllersWithViews();
// 2. Application db context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
// 3. Session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromSeconds(120); });
// 4. Custom services for different tour actions
builder.Services.AddScoped<ITourService, TourService>();


// BUILD THE APP WITH DI CONTAINER
var app = builder.Build();

// ADD MIDDLEWARES WITH THE APP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

// ROUTE MAPPING
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tour}/{action=Index}/{id?}"
);

// RUN THE APP
app.Run();
