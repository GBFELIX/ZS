using EstoqueAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//banco de dados

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// adicionar serviçoes de controle
builder.Services.AddControllers();

// gerar aplicação
var app = builder.Build();

app.UseHttpsRedirection();

//mapear serviços de controle
app.MapControllers();
app.UseAuthorization();
app.Run();