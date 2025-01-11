namespace BibliotecaApi.MessageConstant
{
    public static class MessageConstants
    {
        #region Retorno Reponse
        public const string LivroNaoEncontrado = "Livro não encontrado.";
        public const string LivroAdicionado = "Livro adicionado com sucesso.";
        public const string LivroAtualizado = "Livro atualizado com sucesso.";
        public const string LivroRemovido = "Livro removido com sucesso.";
        public const string LivroIdNuloNegativo = "O id do livro não pode ser nulo ou negativo.";
        public const string LivroJaExiste = "Livro já existe.";
        #endregion

        #region Descrição Endpoints
        public const string BuscaLivroPorId = "Busca um livro pelo seu id.";
        public const string AdicionarLivro = "Adiciona um livro ao acervo.";
        public const string AtualizarLivro = "Atualiza um livro do acervo.";
        public const string RemoverLivro = "Remove um livro do acervo.";
        #endregion
    }
}
