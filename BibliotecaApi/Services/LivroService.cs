using BibliotecaApi.MessageConstant;
using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;
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

                //validar se o livro adicionado já existe no cadastro

                Livro livro =  await _livroRepository.AdicionarLivro(objeto);

                List<Livro> livros = [livro];

                return new LivroResponse(MessageConstants.LivroAdicionadoComSucesso, HttpStatusCode.OK, livros);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<LivroResponse> AtualizarLivro(int id, Livro objeto)
        {
            try
            {
                if (ValidarSeLivroNulo(objeto))
                {
                    return new LivroResponse(MessageConstants.LivroNaoPodeSerAtualizado, HttpStatusCode.BadRequest, []);
                }

                Livro livro = await _livroRepository.AtualizarLivro(id, objeto);

                List<Livro> livros = [livro];

                return new LivroResponse(MessageConstants.LivroAtualizadoComSucesso, HttpStatusCode.OK, livros);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LivroResponse> BuscarLivros()
        {
            try
            {
                IEnumerable<Livro> livros = await _livroRepository.BuscarLivros();

                if (livros == null) 
                {                    
                    return new LivroResponse(MessageConstants.NenhumLivroEncontrado, HttpStatusCode.NotFound, []); ;
                }

                return new LivroResponse(MessageConstants.LivrosEncontrados, HttpStatusCode.OK, livros.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LivroResponse> BuscarLivroPorId(int? id)
        {
            try
            {
                IEnumerable<Livro> livros = await _livroRepository.BuscarLivroPorId(id);
                if (!livros.Any())
                {
                    return new LivroResponse(MessageConstants.LivroNaoEncontrado, HttpStatusCode.NotFound, []);
                }

                return new LivroResponse(MessageConstants.LivrosEncontrados, HttpStatusCode.OK, livros.ToList());

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<LivroResponse> RemoverLivro(int? id)
        {
            try
            {
                IEnumerable<Livro> livros = await _livroRepository.RemoverLivro(id);
                if (!livros.Any())
                {
                    return new LivroResponse(MessageConstants.LivroNaoEncontrado, HttpStatusCode.NotFound, []);
                }

                return new LivroResponse(MessageConstants.LivroRemovidoComSucesso, HttpStatusCode.OK, livros.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool ValidarSeLivroNulo(Livro livro)
        {
            return (livro == null ||
                    string.IsNullOrEmpty(livro.Autor) ||
                    string.IsNullOrEmpty(livro.Editora) ||
                    string.IsNullOrEmpty(livro.Titulo) ||
                    livro.Categoria == null);
        }
    }

}
