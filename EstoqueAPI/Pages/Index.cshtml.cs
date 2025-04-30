using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueAPI.Pages
{
    public class IndexModel : PageModel
    {
        public class Produto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public int Quantidade { get; set; }
            public decimal Valor { get; set; }
        }

        [BindProperty]
        public Produto NovoProduto { get; set; } = new Produto();

        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public string? ErrorMessage { get; set; }

        // Simulação de banco de dados em memória (static para persistir entre requisições)
        private static List<Produto> Estoque = new List<Produto>();
        private static int ProximoId = 1;

        public void OnGet()
        {
            Produtos = Estoque.ToList();
        }

        public IActionResult OnPostAdicionar()
        {
            if (string.IsNullOrWhiteSpace(NovoProduto.Nome) || NovoProduto.Quantidade <= 0 || NovoProduto.Valor <= 0)
            {
                ErrorMessage = "Todos os campos devem ser preenchidos corretamente.";
                Produtos = Estoque.ToList();
                return Page();
            }

            var existente = Estoque.FirstOrDefault(p => p.Nome.Equals(NovoProduto.Nome, StringComparison.OrdinalIgnoreCase));
            if (existente != null)
            {
                existente.Quantidade += NovoProduto.Quantidade;
                existente.Valor = NovoProduto.Valor; // Atualiza o valor
            }
            else
            {
                NovoProduto.Id = ProximoId++;
                Estoque.Add(new Produto
                {
                    Id = NovoProduto.Id,
                    Nome = NovoProduto.Nome,
                    Quantidade = NovoProduto.Quantidade,
                    Valor = NovoProduto.Valor
                });
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemover()
        {
            var existente = Estoque.FirstOrDefault(p => p.Nome.Equals(NovoProduto.Nome, StringComparison.OrdinalIgnoreCase));
            if (existente != null)
            {
                Estoque.Remove(existente);
            }
            else
            {
                ErrorMessage = "Produto não encontrado.";
            }

            Produtos = Estoque.ToList();
            return Page();
        }
    }
}
