using BibliotecaApi.Helpers;
using BibliotecaApi.Models;
using BibliotecaApi.Repository.Connection;
using BibliotecaApi.Repository.Interfaces;
using Dapper;
using System.Data;

namespace BibliotecaApi.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly PostgreConnection _connection;

        public LivroRepository(PostgreConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AdicionarLivroAsync(Livro livro)
        {
            try
            {
                const string sql = @"INSERT INTO Livros 
                                       (autor, 
                                        num_paginas, 
                                        editora, 
                                        titulo, 
                                        isbn, 
                                        idioma_id, 
                                        gen_literario_id, 
                                        sta_leitura_id, 
                                        url_capa, 
                                        dat_cadastro
                                       )
                                     VALUES 
                                        (@Autor, 
                                         @NumeroPaginas, 
                                         @Editora, 
                                         @Titulo, 
                                         @Isbn, 
                                         @Idioma, 
                                         @GeneroLiterario, 
                                         @StatusLeitura, 
                                         @UrlCapa, 
                                         current_timestamp
                                        )
                                     ON CONFLICT (isbn) DO NOTHING";

                DynamicParameters parametros = new();
                parametros.Add("Autor", livro.Autor);
                parametros.Add("NumeroPaginas", livro.NumeroPaginas);
                parametros.Add("Editora", livro.Editora);
                parametros.Add("Titulo", livro.Titulo);
                parametros.Add("Isbn", livro.Isbn);
                parametros.Add("Idioma", livro.Idioma);
                parametros.Add("GeneroLiterario", livro.GeneroLiterario);
                parametros.Add("StatusLeitura", livro.StatusLeitura);
                parametros.Add("UrlCapa", livro.UrlCapa);

                using IDbConnection connection = _connection.Connection;                
                int linhasAfetadas = await connection.ExecuteAsync (sql, parametros);

                return linhasAfetadas > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AtualizarLivroAsync(string? isbn, Livro livro)
        {
			try
			{
				Dictionary<string, object?> camposParaAtualizar = new Dictionary<string, object?>
                {
			        { "autor", livro.Autor },
			        { "editora", livro.Editora },
			        { "titulo", livro.Titulo },
			        { "isbn", livro.Isbn },
			        { "idioma_id", livro.Idioma },
			        { "gen_literario_id", livro.GeneroLiterario },
			        { "num_paginas", livro.NumeroPaginas },
			        { "sta_leitura_id", livro.StatusLeitura }
		        };

				(string sql, DynamicParameters parametros) = SqlUpdateHelper.MontarUpdate<Livro>("livros", "isbn", isbn, camposParaAtualizar);

				using IDbConnection connection = _connection.Connection;
				int linhasAfetadas = await connection.ExecuteAsync(sql, parametros);

				return linhasAfetadas > 0;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async Task<Livro> BuscarLivroPorIsbnAsync(string isbn)
        {
            try
            {
                const string sql = @"SELECT                                    
                                       id               as Id,
                                       autor            as Autor,
                                       editora          as Editora,
                                       titulo           as Titulo,
                                       isbn             as Isbn,
                                       idioma_id        as Idioma,
                                       gen_literario_id as GeneroLiterario,
                                       num_paginas      as NumeroPaginas, 
                                       sta_leitura_id   as StatusLeitura,
                                       urlCapa          as UrlCapa
                                     FROM livros
                                     WHERE isbn = @isbn";

                DynamicParameters parametros = new ();
                parametros.Add ("isbn", isbn);

                using IDbConnection connection = _connection.Connection;

				return await connection.QueryFirstOrDefaultAsync<Livro>(sql, parametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Livro>> BuscarLivrosAsync()
        {
            try
            {
                const string sql = @"SELECT
                                       id               as Id,
                                       autor            as Autor,
                                       editora          as Editora,
                                       titulo           as Titulo,
                                       isbn             as Isbn,
                                       idioma_id        as Idioma,
                                       gen_literario_id as GeneroLiterario,
                                       num_paginas      as NumeroPaginas,
                                       sta_leitura_id   as StatusLeitura,
                                       dat_cadastro     as DataCadastro,
                                       url_Capa         as UrlCapa
                                     FROM livros";

                using IDbConnection connection = _connection.Connection;

				return await connection.QueryAsync<Livro> (sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletarLivroAsync(string? isbn)
        {
            try
            {
                const string sql = @"DELETE FROM livros 
                                     WHERE isbn = @isbn";

                DynamicParameters parametros = new ();
                parametros.Add ("isbn", isbn);

                using IDbConnection connection = _connection.Connection;
                int linhasAfetadas = await connection.ExecuteAsync (sql, parametros);

                return linhasAfetadas > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
