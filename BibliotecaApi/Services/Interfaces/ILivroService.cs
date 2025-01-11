using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;

namespace BibliotecaApi.Services.Interfaces
{
    public interface ILivroService
    {
        Task<LivroResponse> BuscarLivros();
        Task<LivroResponse> BuscarLivroPorId(int? id);
        Task<LivroResponse> AdicionarLivro(Livro objeto);
        Task<LivroResponse> RemoverLivro(int? id);
        Task<LivroResponse> AtualizarLivro(int id, Livro objeto);
    }
}
