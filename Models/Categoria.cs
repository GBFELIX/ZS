using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;
    }
}
