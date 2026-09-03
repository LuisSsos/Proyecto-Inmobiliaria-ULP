using MVC.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<MVC.Repositories.IRepositorioPropietario, MVC.Repositories.RepositorioPropietario>();
builder.Services.AddScoped<MVC.Repositories.IRepositorioInquilino, MVC.Repositories.RepositorioInquilino>();
builder.Services.AddScoped<MVC.Repositories.IRepositorioTipoInmueble, MVC.Repositories.RepositorioTipoInmueble>();
builder.Services.AddScoped<MVC.Repositories.IRepositorioInmueble, MVC.Repositories.RepositorioInmueble>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
