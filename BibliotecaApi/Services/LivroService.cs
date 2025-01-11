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

        public async Task<LivroResponse> AdicionarLivro(Livro ojeto)
        {            
            try
            {
                if (ValidarSeLivroNulo(ojeto))
                {
                    return new LivroResponse(MessageConstants.LivroNaoPodeSerNulo, HttpStatusCode.BadRequest, []);
                }

                Livro livro =  await _livroRepository.AdicionarLivro(ojeto);

                List<Livro> livros = [livro];

                return new LivroResponse(MessageConstants.LivroAdicionadoComSucesso, HttpStatusCode.OK, livros);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<LivroResponse> AtualizarLivro(int id, Livro livro)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }


        public async Task<LivroResponse> RemoverLivro(int? id)
        {
            throw new NotImplementedException();
        }

        private bool ValidarSeLivroNulo(Livro livro)
        {            
            return (livro == null || livro.Autor == null || livro.Categoria == null || livro.Editora == null || livro.Titulo == null);            
        }
    }

}
