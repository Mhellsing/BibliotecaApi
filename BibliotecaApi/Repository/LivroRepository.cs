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

        public async Task<bool> AdicionarLivro(Livro objeto)
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

        public async Task<bool> AtualizarLivro(string? isbn, Livro objeto)
        {
            try
            {
                string sql = @"UPDATE livros SET ";
                DynamicParameters parametros = new ();

                if (!string.IsNullOrEmpty (objeto.Autor))
                {
                    sql += "autor = @autor, ";
                    parametros.Add ("autor", objeto.Autor);
                }

                if (!string.IsNullOrEmpty (objeto.Editora))
                {
                    sql += "editora = @editora, ";
                    parametros.Add ("editora", objeto.Editora);
                }

                if (!string.IsNullOrEmpty (objeto.Titulo))
                {
                    sql += "titulo = @titulo, ";
                    parametros.Add ("titulo", objeto.Titulo);
                }

                if (!string.IsNullOrEmpty (objeto.Isbn))
                {
                    sql += "isbn = @isbn, ";
                    parametros.Add ("isbn", objeto.Isbn);
                }

                if (objeto.Idioma.HasValue)
                {
                    sql += "idioma_id = @idioma, ";
                    parametros.Add ("idioma", objeto.Idioma);
                }

                if (objeto.GeneroLiterario.HasValue)
                {
                    sql += "gen_literario_id = @generoLiterario, ";
                    parametros.Add ("generoLiterario", objeto.GeneroLiterario);
                }

                if (objeto.NumeroPaginas.HasValue)
                {
                    sql += "num_paginas = @numeroPaginas, ";
                    parametros.Add ("numeroPaginas", objeto.NumeroPaginas);
                }

                if (objeto.StatusLeitura.HasValue)
                {
                    sql += "sta_leitura_id = @statusLeitura, ";
                    parametros.Add ("statusLeitura", objeto.StatusLeitura);
                }

                // Remoção da vírgula extra e finalização da query
                sql = sql.TrimEnd (',', ' ') + " WHERE isbn = @isbn";
                parametros.Add ("isbn", isbn);

                // Caso nenhum campo tenha sido modificado
                if (sql.EndsWith ("SET WHERE isbn = @isbn"))
                {
                    new InvalidOperationException ("Nenhum valor foi fornecido para atualização.");
                }

                // Execução da query
                using IDbConnection connection = _connection.Connection;
                int linhasAfetadas = await connection.ExecuteAsync (sql, parametros);

                return linhasAfetadas > 0;
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public async Task<Livro> BuscarLivroPorIsbn(string isbn)
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

        public async Task<IEnumerable<Livro>> BuscarLivros()
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

        public async Task<bool> DeletarLivro(string? isbn)
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
