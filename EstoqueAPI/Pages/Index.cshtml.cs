using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace EstoqueAPI.Pages
{
    public class IndexModel : PageModel
    {
        public class Produto
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public int Quantidade { get; set; }
            public decimal Valor { get; set; }
        }

        [BindProperty]
        public Produto NovoProduto { get; set; } = new Produto();

        public List<Produto> Produtos { get; set; } = new List<Produto>();

        public string? ErrorMessage { get; set; }

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
                existente.Valor = NovoProduto.Valor; 
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

        public IActionResult OnPostRemover(int produtoId)
        {
            var produtoRemover = Estoque.FirstOrDefault(p => p.Id == produtoId);
            if (produtoRemover != null)
            {
                Estoque.Remove(produtoRemover);
            }
            else
            {
                ErrorMessage = "Produto não encontrado.";
            }

            Produtos = Estoque.ToList();
            return RedirectToPage();
        }

        public IActionResult OnPostPrepararAtualizacao(int produtoId, string produtoNome, int produtoQuantidade, decimal produtoValor)
        {
            NovoProduto = new Produto
            {
                Id = produtoId,
                Nome = produtoNome,
                Quantidade = produtoQuantidade,
                Valor = produtoValor
            };

            return Page();
        }

        public IActionResult OnPostAtualizar()
        {
            if (string.IsNullOrWhiteSpace(NovoProduto.Nome) || NovoProduto.Quantidade <= 0 || NovoProduto.Valor <= 0)
            {
                ErrorMessage = "Todos os campos devem ser preenchidos corretamente.";
                Produtos = Estoque.ToList();
                return Page();
            }

            var produtoExistente = Estoque.FirstOrDefault(p => p.Id == NovoProduto.Id);
            if (produtoExistente != null)
            {
                produtoExistente.Quantidade = NovoProduto.Quantidade;
                produtoExistente.Valor = NovoProduto.Valor;
            }
            else
            {
                ErrorMessage = "Produto não encontrado para atualização.";
            }

            Produtos = Estoque.ToList();
            return RedirectToPage();
        }
    }
}
