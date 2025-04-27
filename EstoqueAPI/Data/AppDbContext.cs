using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Models;

namespace EstoqueAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ItemEstoque> ItensEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais para o modelo ItemEstoque, se necessário
            modelBuilder.Entity<ItemEstoque>(entity =>
            {
                entity.HasKey(e => e.Id); // Configura a chave primária
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100); // Exemplo de configuração de propriedade
                entity.Property(e => e.Quantidade).IsRequired();
            });
        }
    }
}