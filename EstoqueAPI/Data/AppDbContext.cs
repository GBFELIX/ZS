using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Models;

namespace EstoqueAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemEstoque> Estoque { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}