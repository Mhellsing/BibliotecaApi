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
        public async Task<IActionResult> BuscarLivroPorId(string? isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {                
                return NotFound(new LivroResponse(MessageConstants.IsbnNaoPodeSerNulo, HttpStatusCode.BadRequest, []));
            }

            LivroResponse response = await _livroService.BuscarLivroPorIsbn(isbn);

            return Ok(response);
        }

        [HttpPost]
        [Route("AdicionarLivro")]
        [EndpointDescription(MessageConstants.AdicionarLivro)]
        public async Task<IActionResult> AdicionarLivro([FromBody] Livro livro)
        {            
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(livro.Id);
            if (validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = await _livroService.AdicionarLivro(livro);

            return Ok(response);
        }

        [HttpDelete]
        [Route("RemoverLivro")]
        [EndpointDescription(MessageConstants.RemoverLivro)]
        public async Task<IActionResult> RemoverLivro(int? id)
        {
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(id);

            if (validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = await _livroService.DeletarLivro(id);

            return Ok(response);
        }

        [HttpPut]
        [Route("AtualizarLivro")]
        [EndpointDescription(MessageConstants.AtualizarLivro)]
        public async Task<IActionResult> AtualizarLivro(int id, [FromBody] Livro livro)
        {
            IActionResult? validacaoResult = ValidarIdSeIdNuloNegativo(id);

            if (validacaoResult != null)
            {
                return validacaoResult;
            }

            LivroResponse response = await _livroService.AtualizarLivro(id, livro);

            return Ok(response);
        }

        private BadRequestObjectResult? ValidarIdSeIdNuloNegativo(int? id)
        {
            if (id == null || id <= 0)
            {                
                return BadRequest(new LivroResponse(MessageConstants.LivroIdNuloNegativo, HttpStatusCode.BadRequest, []));
            }

            return null;
        }
    }
}
