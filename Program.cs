using MVC.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

LoadLocalEnvironmentFile();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<MVC.Repositories.IRepositorioPropietario, MVC.Repositories.RepositorioPropietario>();
builder.Services.AddScoped<MVC.Repositories.IRepositorioInquilino, MVC.Repositories.RepositorioInquilino>();
builder.Services.AddScoped<MVC.Repositories.IRepositorioTipoInmueble, MVC.Repositories.RepositorioTipoInmueble>();
builder.Services.AddScoped<MVC.Repositories.IRepositorioInmueble, MVC.Repositories.RepositorioInmueble>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IRepositorioImagenInmueble, RepositorioImagenInmueble>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccesoDenegado";
    });

var app = builder.Build();
app.UseMiddleware<MVC.Middleware.ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

// ASP.NET Core carga variables del sistema, pero no archivos .env por sí solo.
// Este cargador se usa para el desarrollo local y no reemplaza variables que
// ya hayan sido definidas por el sistema o por el servidor de producción.
static void LoadLocalEnvironmentFile()
{
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    if (!File.Exists(envPath))
    {
        return;
    }

    foreach (var rawLine in File.ReadLines(envPath))
    {
        var line = rawLine.Trim();
        if (line.Length == 0 || line.StartsWith('#'))
        {
            continue;
        }

        var separatorIndex = line.IndexOf('=');
        if (separatorIndex <= 0)
        {
            continue;
        }

        var key = line[..separatorIndex].Trim();
        var value = line[(separatorIndex + 1)..].Trim().Trim('"', '\'');

        if (Environment.GetEnvironmentVariable(key) is null)
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}
