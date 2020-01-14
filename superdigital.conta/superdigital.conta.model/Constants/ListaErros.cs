
namespace superdigital.conta.model.Constants
{
    public static class ListaErros
    {
        
        public const string ErroGenerico = "Falha no servidor";
        public const string DocumentoJaCadastrado = "Documento já cadastrado";
        public const string SaldoInsuficiente = "Este cliente não possui saldo para transferencia";
        public const string ContaOrigemNaoEncontrada = "Conta de origem não existe";
        public const string ContaDestinoNaoEncontrada = "Conta de destino não existe";
        public const string ParametrosNaoPodemSerVazio = "Todos os campos são obrigatórios";
        public const string OrigemDestinoNaoPodemSerIguais = "Contas de Origem e Destino não podem ser iguais";
        public const string DocumentoInvalido = "Documento Invalido";
    }
}
