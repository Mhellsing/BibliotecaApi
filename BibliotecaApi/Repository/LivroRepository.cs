using BibliotecaApi.Enums;
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

        public async Task<bool> AdicionarLivroAsync(Livro objeto)
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
                parametros.Add("Autor", objeto.Autor);
                parametros.Add("NumeroPaginas", objeto.NumeroPaginas);
                parametros.Add("Editora", objeto.Editora);
                parametros.Add("Titulo", objeto.Titulo);
                parametros.Add("Isbn", objeto.Isbn);
                parametros.Add("Idioma", objeto.Idioma);
                parametros.Add("GeneroLiterario", objeto.GeneroLiterario);
                parametros.Add("StatusLeitura", objeto.StatusLeitura);
                parametros.Add("UrlCapa", objeto.UrlCapa);

                using IDbConnection connection = _connection.Connection;                
                int linhasAfetadas = await connection.ExecuteAsync (sql, parametros);

                return linhasAfetadas > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AtualizarLivroAsync(string? isbn, Livro objeto)
        {
            try
			{
				string sql = @"UPDATE livros SET ";
				DynamicParameters parametros = new();

				Dictionary<string, object> camposParaAtualizar = new Dictionary<string, object>
				{
					{ "autor", objeto.Autor },
					{ "editora", objeto.Editora },
					{ "titulo", objeto.Titulo },
					{ "isbn", objeto.Isbn },
					{ "idioma_id", objeto.Idioma },
					{ "gen_literario_id", objeto.GeneroLiterario },
					{ "num_paginas", objeto.NumeroPaginas },
					{ "sta_leitura_id", objeto.StatusLeitura }
				};

				sql = AdicionarCamposParaAtualizar(sql, parametros, camposParaAtualizar);
				sql = sql.TrimEnd(',', ' ') + " WHERE isbn = @isbn";
				
                parametros.Add("isbn", isbn);

				if (sql.EndsWith("SET WHERE isbn = @isbn"))
				{
					return false;
				}


				using IDbConnection connection = _connection.Connection;
				int linhasAfetadas = await connection.ExecuteAsync(sql, parametros);

				return linhasAfetadas > 0;
			}
			catch (Exception ex)
            {
                throw;
            }
        }

		public async Task<Livro> BuscarLivroPorIsbnAsync(string isbn)
        {
            try
            {
                const string sql = @"SELECT                                    
                                       autor            as Autor,
                                       editora          as Editora,
                                       titulo           as Titulo,
                                       isbn             as Isbn,
                                       idioma_id        as Idioma,
                                       gen_literario_id as GeneroLiterario,
                                       num_paginas      as NumeroPaginas, 
                                       sta_leitura_id   as StatusLeitura
                                     FROM livros
                                     WHERE isbn = @isbn";

                DynamicParameters parametros = new ();
                parametros.Add ("isbn", isbn);

                using IDbConnection connection = _connection.Connection;

                return await connection.QueryFirstOrDefaultAsync<Livro> (sql, parametros);
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
                                        autor            as Autor,
                                        editora          as Editora,
                                        titulo           as Titulo,
                                        isbn             as Isbn,
                                        idioma_id        as Idioma,
                                        gen_literario_id as GeneroLiterario,
                                        num_paginas      as NumeroPaginas,
                                        sta_leitura_id   as StatusLeitura
                                     FROM livros";

                using IDbConnection connection = _connection.Connection;

                var teste = await connection.QueryAsync<Livro> (sql);

                return teste;
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
                                     WHERE isbn = @isbn
                                     AND sta_leitura_id = @statusLeitura";

                DynamicParameters parametros = new ();
                parametros.Add ("isbn", isbn);
                parametros.Add ("statusLeitura", StatusLeitura.NaoIniciado);

                using IDbConnection connection = _connection.Connection;
                int linhasAfetadas = await connection.ExecuteAsync (sql, parametros);

                return linhasAfetadas > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

		private static string AdicionarCamposParaAtualizar(string sql, DynamicParameters parametros, Dictionary<string, object> camposParaAtualizar)
		{
			foreach (KeyValuePair<string, object> campo in camposParaAtualizar)
			{
				if (campo.Value != null && !(campo.Value is string valor && string.IsNullOrEmpty(valor)))
				{
					sql += $"{campo.Key} = @{campo.Key}, ";
					parametros.Add(campo.Key, campo.Value);
				}
			}

			return sql;
		}
	}
}
