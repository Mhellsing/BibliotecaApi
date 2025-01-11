using BibliotecaApi.MessageConstants;
using BibliotecaApi.Reponses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BibliotecaApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }


        [HttpGet]
        public IActionResult BuscarLivros() 
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Route("BuscarLivroPorId")]
        [EndpointDescription(MessageConstant.BuscaLivroPorId)]

        public IActionResult BuscarLivroPorId(int? id)
        {
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(id);

            if(validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = _livroService.BuscarLivroPorId(id);

            return Ok(response);
        }        

        [HttpPost]
        [Route("AdicionarLivro")]
        [EndpointDescription(MessageConstant.AdicionarLivro)]
        public IActionResult AdicionarLivro([FromBody] Livro livro)
        {
            // Verificar se o livro já existe
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(livro.Id);
            if(validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = _livroService.AdicionarLivro(livro);
            
            return Ok(response);
        }

        [HttpDelete]
        [Route("RemoverLivro")]
        [EndpointDescription(MessageConstant.RemoverLivro)]
        public IActionResult RemoverLivro(int? id)
        {
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(id);

            if(validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = _livroService.RemoverLivro(id);

            return Ok(response);
        }

        [HttpPut]
        [Route("AtualizarLivro")]
        [EndpointDescription(MessageConstant.AtualizarLivro)]
        public IActionResult AtualizarLivro(int id, [FromBody] Livro livro)
        {
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(id);
            
            if(validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = _livroService.AtualizarLivro(id, livro);

            return Ok(response);
        }

        private BadRequestObjectResult? ValidarIdSeIdNuloNegativo(int? id)
        {
            if(id == null || id <= 0)
            {
                LivroResponse response = new() { StatusCode = HttpStatusCode.BadRequest, Mensagem = MessageConstant.LivroIdNuloNegativo };
                
                return BadRequest(response);
            }

            return null;
        }
    }
}
