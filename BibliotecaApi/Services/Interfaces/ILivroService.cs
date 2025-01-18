using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;

namespace BibliotecaApi.Services.Interfaces
{
    public interface ILivroService
    {
        Task<LivroResponse> AdicionarLivroAsync(Livro objeto);
        Task<LivroResponse> AtualizarLivroAsync(string isbn, Livro objeto);
        Task<LivroResponse> BuscarLivrosAsync();
        Task<LivroResponse> BuscarLivroPorIsbnAsync(string? isbn);
        Task<LivroResponse> DeletarLivroAsync(string? isbn);
    }
}
