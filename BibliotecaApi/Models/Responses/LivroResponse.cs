using System.Net;

namespace BibliotecaApi.Models.Responses
{
    public class LivroResponse
    {
        public string Mensagem { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<Livro> Livros { get; set; }
    }
}
