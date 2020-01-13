
namespace superdigital.conta.model.Interfaces
{
    public interface IContaCorrenteRepository
    {
        void AdicionarContaCorrente(ContaCorrente conta);
        void AtualizarSaldo(ContaCorrente conta);
        ContaCorrente BuscarContaCorrentePorNumeroConta(string numerocontacorrente);
        ContaCorrente BuscarContaCorrentePorDocumento(string documento);
    }
}
