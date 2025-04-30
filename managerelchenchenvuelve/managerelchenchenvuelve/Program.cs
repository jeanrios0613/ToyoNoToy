using managerelchenchenvuelve.Models;
using managerelchenchenvuelve.Models.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using managerelchenchenvuelve.Services;
using managerelchenchenvuelve.Controllers;
using managerelchenchenvuelve.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

//inyectar las base de datos el proyecto para reutilizar en cualquier controlador. 
builder.Services.AddTransient<DatabaseConnection>();
builder.Services.AddTransient<DatabaseServerAdmin>();

// Configuración de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ToyoNoToyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

builder.Services.AddDbContext<IdentityServerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexionDb"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

builder.Services.AddIdentity<managerelchenchenvuelve.Models.Interface.User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServerContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();  
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
