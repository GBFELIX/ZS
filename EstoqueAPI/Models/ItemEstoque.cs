using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstoqueAPI.Models
{
    public class ItemEstoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        // Chave estrangeira para a categoria
        public int CategoriaId { get; set; }

        // Propriedade de navegação para Categoria
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } = null!;
    }
}
