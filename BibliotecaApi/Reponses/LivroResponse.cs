using System.Net;

namespace BibliotecaApi.Reponses
{
    public class LivroResponse
    {
        public string Mensagem { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IEnumerable<Livro> Livros { get; set; }
    }
}
