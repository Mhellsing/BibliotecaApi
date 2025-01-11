using BibliotecaApi.Models.Base;

namespace BibliotecaApi.Models
{
    public class Autor : BaseModel
    {
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Nacionalidade { get; set; }        
    }
}
