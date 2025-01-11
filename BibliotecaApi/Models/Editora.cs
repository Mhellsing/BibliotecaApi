using BibliotecaApi.Models.Base;

namespace BibliotecaApi.Models
{
    public class Editora : BaseModel 
    {
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string WebSite { get; set; }
    }
}
