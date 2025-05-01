using EstoqueAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Serviços necessários
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("EstoqueAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5043/");
});

var app = builder.Build();

// Middleware na ordem correta
app.UseHttpsRedirection();
app.UseStaticFiles(); // <-- importante se você tiver CSS ou JS
app.UseRouting();

// Mapeamento de rotas
app.MapRazorPages();    // Razor Pages
app.MapControllers();   // API Controllers

app.Run();
