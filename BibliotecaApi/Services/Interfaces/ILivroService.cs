using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;

namespace BibliotecaApi.Services.Interfaces
{
    public interface ILivroService
    {
        Task<LivroResponse> AdicionarLivro(Livro objeto);
        Task<LivroResponse> AtualizarLivro(string isbn, Livro objeto);
        Task<LivroResponse> BuscarLivros();        
        Task<LivroResponse> BuscarLivroPorIsbn(string? isbn);
        Task<LivroResponse> DeletarLivro(string? isbn);
    }
}
