using EstoqueAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// adicionar serviçoes de controle
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("EstoqueAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5043/"); // ou http://localhost:5000/
});

var app = builder.Build();

app.UseRouting();

//mapear serviços de controle
app.MapControllers();
//mapear serviços de paginação
app.MapRazorPages();

app.UseHttpsRedirection();


app.Run();