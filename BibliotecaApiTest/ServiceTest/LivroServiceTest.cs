using AutoFixture;
using BibliotecaApi.MessageConstant;
using BibliotecaApi.Models;
using BibliotecaApi.Models.Responses;
using BibliotecaApi.Repository.Interfaces;
using BibliotecaApi.Services;
using BibliotecaApi.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Npgsql;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System.Net;

namespace BibliotecaApiTest.ServiceTest
{
	public class LivroServiceTest
    {
        private readonly ILivroRepository _repository = Substitute.For<ILivroRepository>();
        private readonly ILogger<LivroService> _logger = Substitute.For<ILogger<LivroService>>();
        private readonly ILivroService _service;
        private readonly IFixture _fixture;
        public LivroServiceTest()
        {
            _service = new LivroService(_repository, _logger);
            _fixture = new Fixture();
        }

        [Fact]
        public async void RetornaResponseComMensagemLivroAdicionadoComSucessoStatusCode200EListaComObjetoAdicionado()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            List<Livro> livros = new List<Livro>() { livro };

            _repository.AdicionarLivroAsync(livro).ReturnsForAnyArgs(true);

            //Act
            LivroResponse result = await _service.AdicionarLivroAsync(livro);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.LivroAdicionadoComSucesso);
                      x.StatusCode.Should().Be(HttpStatusCode.OK);
                      x.Livros.Should().BeEquivalentTo(livros);
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemLivroJaCadastradoStatusCode400EListaVaziaAoInserirLivroJaCadastrado()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            _repository.AdicionarLivroAsync(livro).ReturnsForAnyArgs(false);

            //Act
            LivroResponse result = await _service.AdicionarLivroAsync(livro);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.LivroJaCadastrado);
                      x.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                      x.Livros.Should().BeEmpty();
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemLivroNaoPodeSerNuloStatusCode400EListaVaziaAoInserirLivroNulo()
        {
            //Arrange
            Livro livro = null;

            //Act
            LivroResponse result = await _service.AdicionarLivroAsync(livro);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.LivroNaoPodeSerNulo);
                      x.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                      x.Livros.Should().BeEmpty();
                  });
        }

        [Fact]
        public async void RetornResponseComMensagemErroInternoQuandoExceptionForLancada()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            List<Livro> livros = new List<Livro>() { livro };
            _repository.AdicionarLivroAsync(livro).ThrowsAsyncForAnyArgs(new NpgsqlException());

            //Act
            LivroResponse result = await _service.AdicionarLivroAsync(livro);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.ErroInterno);
                      x.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
                      x.Livros.Should().BeEmpty();
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemLivroAtualizadoComSucessoStatusCode200EListaComObjetoAtualizado()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            List<Livro> livros = new List<Livro>() { livro };
            _repository.AtualizarLivroAsync(livro.Isbn, livro).ReturnsForAnyArgs(true);

            //Act
            LivroResponse result = await _service.AtualizarLivroAsync(livro.Isbn, livro);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.LivroAtualizadoComSucesso);
                      x.StatusCode.Should().Be(HttpStatusCode.OK);
                      x.Livros.Should().BeEquivalentTo(livros);
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemLivroNaoEncontradoStatusCode404EListaVaziaAoAtualizarLivroNaoCadastrado()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            List<Livro> livros = new List<Livro>() { livro };
            _repository.AtualizarLivroAsync(livro.Isbn, livro).ReturnsForAnyArgs(false);

            //Act
            LivroResponse result = await _service.AtualizarLivroAsync(livro.Isbn, livro);

            //Assert
            result.Should()
                .NotBeNull()
                .And
                .Satisfy<LivroResponse>(x =>
                {
                    x.Mensagem.Should().Be(MessageConstants.LivroNaoEncontrado);
                    x.StatusCode.Should().Be(HttpStatusCode.NotFound);
                    x.Livros.Should().BeEmpty();
                });
        }

        [Fact]
        public async void RetornaResponseComMensagemErroInternoQuandoExceptionForLancadaAoAtualizarLivro()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            List<Livro> livros = new List<Livro>() { livro };
            _repository.AtualizarLivroAsync(livro.Isbn, livro).ThrowsAsyncForAnyArgs(new InvalidOperationException());

            //Act
            LivroResponse result = await _service.AtualizarLivroAsync(livro.Isbn, livro);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.ErroInterno);
                      x.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
                      x.Livros.Should().BeEmpty();
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemLivrosEncontradosStatusCode200EListaComLivros()
        {
            //Arrange
            List<Livro> livros = _fixture.CreateMany<Livro>().ToList();
            _repository.BuscarLivrosAsync().ReturnsForAnyArgs(livros);

            //Act
            LivroResponse result = await _service.BuscarLivrosAsync();

            //Assert
            result.Should()
                .NotBeNull()
                .And
                .Satisfy<LivroResponse>(x =>
                {
                    x.Mensagem.Should().Be(MessageConstants.LivrosEncontrados);
                    x.StatusCode.Should().Be(HttpStatusCode.OK);
                    x.Livros.Should().BeEquivalentTo(livros);
                });
        }

        [Fact]
        public async void RetornaResponseComMensagemNenhumLivroEncontradoStatusCode404EListaVaziaAoBuscarLivros()
        {
            //Arrange
            _repository.BuscarLivrosAsync().ReturnsForAnyArgs(new List<Livro>());

            //Act
            LivroResponse result = await _service.BuscarLivrosAsync();

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.NenhumLivroEncontrado);
                      x.StatusCode.Should().Be(HttpStatusCode.NotFound);
                      x.Livros.Should().BeEmpty();
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemErroInternoQuandoExceptionForLancadaAoBuscarLivros()
        {
            //Arrange
            _repository.BuscarLivrosAsync().ThrowsAsyncForAnyArgs(new Exception());

            //Act
            LivroResponse result = await _service.BuscarLivrosAsync();

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.ErroInterno);
                      x.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
                      x.Livros.Should().BeEmpty();
                  });
        }

        [Fact]
        public async void RetornaResponseComMensagemLivrosEncontradosStatusCode200EListaComLivro()
        {
            //Arrange
            Livro livro = _fixture.Create<Livro>();
            List<Livro> livros = new List<Livro>() { livro };

            _repository.BuscarLivroPorIsbnAsync(livro.Isbn).ReturnsForAnyArgs(livro);

            //Act
            LivroResponse result = await _service.BuscarLivroPorIsbnAsync(livro.Isbn);

            //Assert
            result.Should()
                  .NotBeNull()
                  .And
                  .Satisfy<LivroResponse>(x =>
                  {
                      x.Mensagem.Should().Be(MessageConstants.LivrosEncontrados);
                      x.StatusCode.Should().Be(HttpStatusCode.OK);
                      x.Livros.Should().BeEquivalentTo(livros);
                  });
        }

		[Fact]
		public async void RetornaResponseComMensagemLivroNaoEncontradosStatusCode404EListaVazia()
		{
			//Arrange
            Livro livro = new Livro();
			List<Livro> livros = new List<Livro>() {};

			_repository.BuscarLivroPorIsbnAsync(livro.Isbn).ReturnsNullForAnyArgs();

			//Act
			LivroResponse result = await _service.BuscarLivroPorIsbnAsync(livro.Isbn);

			//Assert
			result.Should()
				  .NotBeNull()
				  .And
				  .Satisfy<LivroResponse>(x =>
				  {
					  x.Mensagem.Should().Be(MessageConstants.LivroNaoEncontrado);
					  x.StatusCode.Should().Be(HttpStatusCode.NotFound);
					  x.Livros.Should().BeEquivalentTo(livros);
				  });
		}
	}
}