
using System.Threading.Tasks;

namespace superdigital.conta.model.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task AdicionarContaCorrente(ContaCorrente conta);
        Task AtualizarSaldo(ContaCorrente conta);
        Task<ContaCorrente> BuscarContaCorrentePorNumeroConta(string numerocontacorrente);
        Task<ContaCorrente> BuscarContaCorrentePorDocumento(string documento);
    }
}
