using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.Models
{
    public class ItemEstoque{
        
        [Key]
        public string Nome{ get; set;} =  string.Empty;
        public int Quantidade {get; set;}

        public int Valor {get; set;}
    }
}