using System.Net;

namespace BibliotecaApi.Models.Responses
{
    public class LivroResponse
    {
        public string Mensagem { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<Livro> Livros { get; set; }

        public LivroResponse(string mensagem, HttpStatusCode statusCode, List<Livro> livros) 
        {
            Mensagem = mensagem;
            StatusCode = statusCode;
            Livros = livros;
        }
    }
}
