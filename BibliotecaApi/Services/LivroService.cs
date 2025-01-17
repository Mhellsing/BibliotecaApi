using BibliotecaApi.MessageConstant;
using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;
using BibliotecaApi.Repository.Interfaces;
using BibliotecaApi.Services.Interfaces;
using System.Net;

namespace BibliotecaApi.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<LivroResponse> AdicionarLivro(Livro objeto)
        {            
            try
            {
                if (ValidarSeLivroNulo(objeto))
                {
                    return new LivroResponse(MessageConstants.LivroNaoPodeSerNulo, HttpStatusCode.BadRequest, []);
                }               

                if(string.IsNullOrEmpty(objeto.Isbn))
                {
                    return new LivroResponse(MessageConstants.IsbnNaoPodeSerNulo, HttpStatusCode.BadRequest, []);
                }
                
                bool foiAdicionado = await _livroRepository.AdicionarLivro(objeto);

                if (!foiAdicionado)
                {
                    return new LivroResponse (MessageConstants.LivroJaCadastrado, HttpStatusCode.BadRequest, []);
                }                

                return new LivroResponse (MessageConstants.LivroAdicionadoComSucesso, HttpStatusCode.OK, [objeto]); ;
            }
            catch (Exception ex)
            {
                return new LivroResponse(MessageConstants.ErroInterno, HttpStatusCode.InternalServerError, []);
            }            
        }

        public async Task<LivroResponse> AtualizarLivro(string? isbn, Livro objeto)
        {
            try
            {
                bool foiAtualizado = await _livroRepository.AtualizarLivro(isbn, objeto);

                if (!foiAtualizado)
                {
                    return new LivroResponse (MessageConstants.LivroNaoEncontrado, HttpStatusCode.NotFound, []);
                }

                return new LivroResponse(MessageConstants.LivroAtualizadoComSucesso, HttpStatusCode.OK, [objeto]);
            }
            catch (Exception ex)
            {
                return new LivroResponse (MessageConstants.ErroInterno, HttpStatusCode.InternalServerError, []);
            }
        }

        public async Task<LivroResponse> BuscarLivros()
        {
            try
            {
                IEnumerable<Livro> livros = await _livroRepository.BuscarLivros();

                if (!livros.Any()) 
                {                       
                    return new LivroResponse(MessageConstants.NenhumLivroEncontrado, HttpStatusCode.NotFound, []); ;
                }

                return new LivroResponse(MessageConstants.LivrosEncontrados, HttpStatusCode.OK, livros.ToList());
            }
            catch (Exception)
            {
                return new LivroResponse (MessageConstants.ErroInterno, HttpStatusCode.InternalServerError, []);
            }
        }

        public async Task<LivroResponse> BuscarLivroPorIsbn(string? isbn)
        {
            try
            {
                Livro? livro = await _livroRepository.BuscarLivroPorIsbn(isbn);

                if (livro == null)
                {
                    return new LivroResponse(MessageConstants.LivroNaoEncontrado, HttpStatusCode.NotFound, []);
                }

                return new LivroResponse(MessageConstants.LivrosEncontrados, HttpStatusCode.OK, [livro]);
            }
            catch (Exception)
            {
                return new LivroResponse (MessageConstants.ErroInterno, HttpStatusCode.InternalServerError, []);
            }
        }

        public async Task<LivroResponse> DeletarLivro(string? isbn)
        {
            try
            {
                bool foiDeletado = await _livroRepository.DeletarLivro(isbn);

                if (!foiDeletado)
                {
                    return new LivroResponse(MessageConstants.LivroNaoEncontrado, HttpStatusCode.NotFound, []);
                }

                return new LivroResponse(MessageConstants.LivroRemovidoComSucesso, HttpStatusCode.OK, []);
            }
            catch (Exception)
            {
                return new LivroResponse (MessageConstants.ErroInterno, HttpStatusCode.InternalServerError, []);
            }
        }

        private static bool ValidarSeLivroNulo(Livro livro)
        {
            return (livro == null ||
                    string.IsNullOrEmpty(livro.Autor) ||
                    string.IsNullOrEmpty(livro.Editora) ||
                    string.IsNullOrEmpty(livro.Titulo) ||
                    !livro.GeneroLiterario.HasValue);
        }
    }
}
