using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Models;

namespace EstoqueAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ItemEstoque> Estoque { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento entre ItemEstoque e Categoria
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(i => i.Categoria) // Cada ItemEstoque tem uma Categoria
                .WithMany() // Uma Categoria pode ter muitos ItemEstoques
                .HasForeignKey(i => i.CategoriaId); // Chave estrangeira é CategoriaId
        }
    }
}