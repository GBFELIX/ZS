using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

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

            // Relacionamento entre ItemEstoque e Categoria
            modelBuilder.Entity<ItemEstoque>()
                .HasOne(i => i.Categoria)
                .WithMany()
                .HasForeignKey(i => i.CategoriaId);
        }
    }
}
