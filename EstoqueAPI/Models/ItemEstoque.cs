using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.Models
{
    public class ItemEstoque{
        [Key]
        public int Id{ get; set;}   
        [Required]
        public string Nome{ get; set;} =  string.Empty;
        [Required]
        public int Quantidade {get; set;}
        [Required]
        public decimal Valor {get; set;}
    }
}