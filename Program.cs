using System;
using EstoqueAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do db
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Servi�os obrigatorios
builder.Services.AddControllers();
builder.Services.AddRazorPages();


var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();

// Mapeamento de rotas
app.MapRazorPages();    // Razor Pages
app.MapControllers();   // API Controllers

app.Run();