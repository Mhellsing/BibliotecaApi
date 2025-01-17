using BibliotecaApi.Models;

namespace BibliotecaApi.Repository.Interfaces
{
    public interface ILivroRepository
    {
        Task<bool> AdicionarLivro(Livro objeto);
        Task<bool> AtualizarLivro(string? isbn, Livro objeto);
        Task<IEnumerable<Livro>> BuscarLivros();        
        Task<Livro> BuscarLivroPorIsbn(string isbn);
        Task<bool> DeletarLivro(string? isbn);

    }
}
