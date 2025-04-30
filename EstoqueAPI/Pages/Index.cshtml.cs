using EstoqueAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

public class EstoqueModel : PageModel
{
    private readonly HttpClient _httpClient;

    public List<ItemEstoque> ItensEstoque { get; set; } = new();

    public EstoqueModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("EstoqueAPI");
    }

    public async Task OnGetAsync()
    {
        var resultado = await _httpClient.GetFromJsonAsync<List<ItemEstoque>>("api/estoque");
        if (resultado != null)
        {
            ItensEstoque = resultado;
        }
    }
}
