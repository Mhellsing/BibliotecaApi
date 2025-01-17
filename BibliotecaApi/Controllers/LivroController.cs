using BibliotecaApi.MessageConstant;
using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;
using BibliotecaApi.Services.Interfaces;
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

        [HttpPost]
        [Route("AdicionarLivro")]
        [EndpointDescription(MessageConstants.AdicionarLivro)]
        public async Task<IActionResult> AdicionarLivro([FromBody] Livro livro)
        {
            LivroResponse response = await _livroService.AdicionarLivro(livro);

            return Ok(response);
        }

        [HttpPost]
        [Route("AtualizarLivro")]
        [EndpointDescription(MessageConstants.AtualizarLivro)]
        public async Task<IActionResult> AtualizarLivro(string isbn, [FromBody] Livro livro)
        {
            ValidarSeIsbnNulo (isbn);

            LivroResponse response = await _livroService.AtualizarLivro(isbn, livro);

            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarLivros")]
        [EndpointDescription(MessageConstants.BuscaTodos)]
        public async Task<IActionResult> BuscarLivros()
        {
            LivroResponse response = await _livroService.BuscarLivros();
            
            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarLivroPorIsbn")]
        [EndpointDescription(MessageConstants.BuscaLivroPorIsbn)]
        public async Task<IActionResult> BuscarLivroPorIsbn(string? isbn)
        {
            ValidarSeIsbnNulo (isbn);

            LivroResponse response = await _livroService.BuscarLivroPorIsbn(isbn);

            return Ok(response);
        }        

        [HttpDelete]
        [Route("DeletarLivro")]
        [EndpointDescription(MessageConstants.DeletarLivro)]
        public async Task<IActionResult> DeletarLivro(string? isbn)
        {
            ValidarSeIsbnNulo(isbn);

            LivroResponse response = await _livroService.DeletarLivro(isbn);

            return Ok(response);
        }       

        private BadRequestObjectResult? ValidarSeIsbnNulo(string? isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {                
                return BadRequest(new LivroResponse(MessageConstants.IsbnNaoPodeSerNulo, HttpStatusCode.BadRequest, []));
            }

            return null;
        }
    }
}
