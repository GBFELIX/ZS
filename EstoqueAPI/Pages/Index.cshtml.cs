using EstoqueAPI.Data; // substitua pelo namespace correto do seu DbContext
using EstoqueAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemEstoque NovoProduto { get; set; } = new();

        public List<ItemEstoque> Produtos { get; set; } = [];
        public List<Categoria> ListaCategorias { get; set; } = [];

        public string? ErrorMessage { get; set; }
        public string? RightMessage { get; set; }

        public void OnGet()
        {
            Produtos = _context.Estoque.Include(p => p.Categoria).ToList();
            ListaCategorias = _context.Categorias.ToList();
        }

        public IActionResult OnPostAdicionar()
        {
            ListaCategorias = _context.Categorias.ToList();

            if (!ModelState.IsValid || NovoProduto.CategoriaId == null)
            {
                ErrorMessage = "Todos os campos devem ser preenchidos corretamente.";
                Produtos = _context.Estoque.Include(p => p.Categoria).ToList();
                return Page();
            }

            _context.Estoque.Add(NovoProduto);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostRemover(int produtoId)
        {
            var produto = _context.Estoque.Find(produtoId);
            if (produto != null)
            {
                _context.Estoque.Remove(produto);
                _context.SaveChanges();
            }
            else
            {
                ErrorMessage = "Produto não encontrado.";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostPrepararAtualizacao(int produtoId)
        {
            var produto = _context.Estoque.Find(produtoId);
            if (produto != null)
            {
                NovoProduto = produto;
                ListaCategorias = _context.Categorias.ToList();
                Produtos = _context.Estoque.Include(p => p.Categoria).ToList();
                return Page();
            }

            ErrorMessage = "Produto não encontrado.";
            return RedirectToPage();
        }

        public IActionResult OnPostAtualizar()
        {
            ListaCategorias = _context.Categorias.ToList();

            if (!ModelState.IsValid || NovoProduto.CategoriaId == null)
            {
                ErrorMessage = "Todos os campos devem estar preenchidos corretamente.";
                Produtos = _context.Estoque.Include(p => p.Categoria).ToList();
                return Page();
            }

            var produtoExistente = _context.Estoque.Find(NovoProduto.Id);
            if (produtoExistente != null)
            {
                produtoExistente.Nome = NovoProduto.Nome;
                produtoExistente.Quantidade = NovoProduto.Quantidade;
                produtoExistente.Valor = NovoProduto.Valor;
                produtoExistente.CategoriaId = NovoProduto.CategoriaId;

                _context.SaveChanges();
            }
            else
            {
                ErrorMessage = "Produto não encontrado para atualização.";
            }

            return RedirectToPage();
        }
    }
}
