using BibliotecaApi.Models;

namespace BibliotecaApi.Repository.Interfaces
{
    public interface ILivroRepository
    {
        Task<Livro> AdicionarLivro(Livro objeto);
        Task<bool> AtualizarLivro(int? id, Livro objeto);
        Task<IEnumerable<Livro>> BuscarLivros();        
        Task<Livro> BuscarLivroPorIsbn(string? isbn);
        Task<bool> DeletarLivro(int? id);

    }
}
