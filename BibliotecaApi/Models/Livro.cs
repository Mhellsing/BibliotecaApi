using BibliotecaApi.Enums;
using BibliotecaApi.Models.Base;

namespace BibliotecaApi.Models
{
    public class Livro : BaseModel
    {        
        public int NumeroPaginas { get; set; }
        public string? Autor { get; set; }
        public string? Editora { get; set; }
        public string? Titulo { get; set; }
        public string? CapaUrl { get; set; }
        public string? Isbn { get; set; }
        public Idioma? Idioma { get; set; }
        public GeneroLiterario? GeneroLiterario { get; set; }
        public StatusLeitura? StatusLeitura { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
