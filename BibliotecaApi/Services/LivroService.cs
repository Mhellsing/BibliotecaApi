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
        private readonly ILogger<LivroService> _logger;
        
        public LivroService(ILivroRepository livroRepository, ILogger<LivroService> logger)
        {
            _livroRepository = livroRepository;
            _logger = logger;
        }

        public async Task<LivroResponse> AdicionarLivroAsync(Livro objeto)
        {            
            try
            {
                if (ValidarSeLivroNulo(objeto))
                {
                    return new LivroResponse(MessageConstants.LivroNaoPodeSerNulo, HttpStatusCode.BadRequest, []);
                }
                
                bool foiAdicionado = await _livroRepository.AdicionarLivroAsync(objeto);

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

        public async Task<LivroResponse> AtualizarLivroAsync(string? isbn, Livro objeto)
        {
            try
            {
                bool foiAtualizado = await _livroRepository.AtualizarLivroAsync(isbn, objeto);

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

        public async Task<LivroResponse> BuscarLivrosAsync()
        {
            try
            {
                IEnumerable<Livro> livros = await _livroRepository.BuscarLivrosAsync();

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

        public async Task<LivroResponse> BuscarLivroPorIsbnAsync(string? isbn)
        {
            try
            {
                Livro? livro = await _livroRepository.BuscarLivroPorIsbnAsync(isbn);

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

        public async Task<LivroResponse> DeletarLivroAsync(string? isbn)
        {
            try
            {
                bool foiDeletado = await _livroRepository.DeletarLivroAsync(isbn);

                if (!foiDeletado)
                {
                    return new LivroResponse(MessageConstants.LivroNaoPodeSerDeletado, HttpStatusCode.NoContent, []);
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
