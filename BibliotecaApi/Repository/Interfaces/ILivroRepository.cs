using BibliotecaApi.Models;

namespace BibliotecaApi.Repository.Interfaces
{
    public interface ILivroRepository
    {
        Task<bool> AdicionarLivroAsync(Livro objeto);
        Task<bool> AtualizarLivroAsync(string? isbn, Livro objeto);
        Task<IEnumerable<Livro>> BuscarLivrosAsync();        
        Task<Livro> BuscarLivroPorIsbnAsync(string isbn);
        Task<bool> DeletarLivroAsync(string? isbn);
    }
}
