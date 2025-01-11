namespace BibliotecaApi.MessageConstant
{
    public static class MessageConstants
    {
        #region Retorno Reponse
        public const string LivrosEncontrados = "Livros encontrados.";
        public const string LivroNaoEncontrado = "Livro não encontrado.";
        public const string LivroAdicionadoComSucesso = "Livro adicionado com sucesso.";
        public const string LivroAtualizadoComSucesso = "Livro atualizado com sucesso.";
        public const string LivroRemovidoComSucesso = "Livro removido com sucesso.";
        public const string LivroIdNuloNegativo = "O id do livro não pode ser nulo ou negativo.";
        public const string LivroJaExiste = "O livro já existe.";
        public const string LivroNaoPodeSerNulo = "Não é possível adicionar um livro ausente de informações.";
        public const string NenhumLivroEncontrado = "Nenhum livro encontrado.";
        #endregion

        #region Descrição Endpoints
        public const string BuscaTodos = "Buscar todos os livros do acervo.";
        public const string BuscaLivroPorId = "Busca um livro pelo seu id.";
        public const string AdicionarLivro = "Adiciona um livro ao acervo.";
        public const string AtualizarLivro = "Atualiza um livro do acervo.";
        public const string RemoverLivro = "Remove um livro do acervo.";
        #endregion
    }
}
