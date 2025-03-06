using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BarberiaPro.Data;
using BarberiaPro.Context;
using Microsoft.EntityFrameworkCore;
using BarberiaPro.Services;
using BarberiaPro.Models;
using Microsoft.JSInterop;
using Stripe;
using QuestPDF.Infrastructure;
QuestPDF.Settings.License = LicenseType.Community;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.AddScoped<UserStateService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<PerfilClienteService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<CargoService>();
// Configurar HttpClient con la dirección base del backend local
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:7295") // Reemplaza con el puerto de tu backend
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<CitaService>();
var app = builder.Build();
// Usar CORS
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
