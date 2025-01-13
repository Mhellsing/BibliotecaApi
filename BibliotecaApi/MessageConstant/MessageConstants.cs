namespace BibliotecaApi.MessageConstant
{
    public static class MessageConstants
    {
        #region Retorno Reponse
        public const string LivrosEncontrados = "Livro(s) encontrado(s).";
        public const string LivroNaoEncontrado = "Livro não encontrado.";
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
        #endregion

        #region Descrição Endpoints
        public const string BuscaTodos = "Buscar todos os livros do acervo.";
        public const string BuscaLivroPorIsbn = "Busca um livro pelo seu isbn.";
        public const string AdicionarLivro = "Adiciona um livro ao acervo.";
        public const string AtualizarLivro = "Atualiza um livro do acervo.";
        public const string RemoverLivro = "Remove um livro do acervo.";
        #endregion
    }
}
