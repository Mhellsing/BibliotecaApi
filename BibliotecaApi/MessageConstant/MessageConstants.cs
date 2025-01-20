namespace BibliotecaApi.MessageConstant
{
    public static class MessageConstants
    {
        #region Reponse Messages
        public const string LivrosEncontrados = "Livro(s) encontrado(s).";
        public const string LivroNaoEncontrado = "Livro não encontrado.";
        public const string LivroNaoPodeSerDeletado = "Livro não pode ser deletado, verifique se o status de leitura é igual a 'Não Lido' ou se o ISBN está correto.";
        public const string LivroAdicionadoComSucesso = "Livro adicionado com sucesso.";
        public const string LivroAtualizadoComSucesso = "Livro atualizado com sucesso.";
        public const string LivroRemovidoComSucesso = "Livro removido com sucesso.";
        public const string LivroIdNuloNegativo = "O id do livro não pode ser nulo ou negativo.";
        public const string LivroJaCadastrado = "Já existe um cadastro para este livro.";
        public const string LivroNaoPodeSerNulo = "Não é possível adicionar um livro ausente de informações.";
        public const string LivroNaoPodeSerAtualizado = "Não é possível atualizar um livro ausente de informações.";
        public const string NenhumLivroEncontrado = "Nenhum livro encontrado.";        
        public const string LivroNaoEncontradoParaRemocao = "Livro não encontrado para remoção.";
        public const string LivroNaoEncontradoParaAtualizacao = "Livro não encontrado para atualização.";
        public const string IsbnNaoPodeSerNulo = "O isbn do livro não pode ser nulo.";
        public const string IsbnNaoPodeSerZero = "O isbn do livro não pode ser zero.";
        public const string ErroInterno = "Ocorreu um erro interno. Tente novamente.";
        public const string NenhumValorFornecidoParaAtualizacao = "Nenhum valor foi fornecido para atualização.";
		#endregion

		#region Descrição Endpoints
		public const string BuscaTodos = "Buscar todos os livros do acervo.";
        public const string BuscaLivroPorIsbn = "Busca um livro pelo seu isbn.";
        public const string AdicionarLivro = "Adiciona um livro ao acervo.";
        public const string AtualizarLivro = "Atualiza um livro do acervo.";
        public const string DeletarLivro = "Deleta um livro do acervo.";
        #endregion

        #region Log Messages
        public const string AdicionarLivroLog = "Iniciando processo de adição do livro: {titulo} ao acervo.";
        public const string AtualizarLivroLog = "Iniciando processo de atualização do livro: {titulo} do acervo.";
        public const string DeletarLivroLog = "Iniciando processo de deleção do livro: {titulo} do acervo.";
        public const string BuscarLivroPorIsbnLog = "Iniciando processo de busca do livro pelo isbn: {isbn}.";
        public const string BuscarTodosLivrosLog = "Iniciando a busca todos os livros do acervo.";
        #endregion
    }
}
