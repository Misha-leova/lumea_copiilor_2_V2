using lumea_copiilor_2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)) 
    ));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; 
        options.AccessDeniedPath = "/Account/AccessDenied"; 
    });

// Add authorization services
builder.Services.AddAuthorization(options =>
{
    // Define a policy for the "Admin" role
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication(); // Add this before UseAuthorization
app.UseAuthorization();

// Define routes
app.MapControllerRoute(
    name: "Admin",
    pattern: "Admin/{controller}/{action}/{id?}",
    defaults: new { controller = "AdminUsers", action = "Index" }
);

// Default route for public marketplace
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserPages}/{action=First}/{id?}"
);

app.Run();