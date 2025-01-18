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
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILivroService livroService, ILogger<LivroController> logger)
        {
            _livroService = livroService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AdicionarLivro")]
        [EndpointDescription(MessageConstants.AdicionarLivro)]
        public async Task<IActionResult> AdicionarLivroAsync([FromBody] Livro livro)
        {
            _logger.LogInformation(MessageConstants.AdicionarLivroLog, livro.Titulo);
            LivroResponse response = await _livroService.AdicionarLivroAsync(livro);

            return Ok(response);
        }

        [HttpPost]
        [Route("AtualizarLivro")]
        [EndpointDescription(MessageConstants.AtualizarLivro)]
        public async Task<IActionResult> AtualizarLivroAsync(string isbn, [FromBody] Livro livro)
        {
            ValidarSeIsbnNulo (isbn);

            _logger.LogInformation(MessageConstants.AtualizarLivroLog, livro.Titulo);
            LivroResponse response = await _livroService.AtualizarLivroAsync(isbn, livro);

            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarLivros")]
        [EndpointDescription(MessageConstants.BuscaTodos)]
        public async Task<IActionResult> BuscarLivrosAsync()
        {
            _logger.LogInformation(MessageConstants.BuscarTodosLivrosLog);
            LivroResponse response = await _livroService.BuscarLivrosAsync();
            
            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarLivroPorIsbn")]
        [EndpointDescription(MessageConstants.BuscaLivroPorIsbn)]
        public async Task<IActionResult> BuscarLivroPorIsbnAsync(string? isbn)
        {
            ValidarSeIsbnNulo (isbn);

            _logger.LogInformation(MessageConstants.BuscarLivroPorIsbnLog, isbn);
            LivroResponse response = await _livroService.BuscarLivroPorIsbnAsync(isbn);

            return Ok(response);
        }        

        [HttpDelete]
        [Route("DeletarLivro")]
        [EndpointDescription(MessageConstants.DeletarLivro)]
        public async Task<IActionResult> DeletarLivroAsync(string? isbn)
        {
            ValidarSeIsbnNulo(isbn);

            _logger.LogInformation(MessageConstants.DeletarLivroLog, isbn);
            LivroResponse response = await _livroService.DeletarLivroAsync(isbn);

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
