
using superdigital.conta.model.Contracts.ContaCorrente;
using superdigital.conta.model.Results;
using System.Threading.Tasks;

namespace superdigital.conta.model.Interfaces
{
    public interface IContaCorrenteService
    {
        Task<Result> AdicionarContaCorrente(ContaCorrentePostRequest conta);
        Task<Result> AtualizarSaldo(ContaCorrente conta);
        Task<Result<ContaCorrenteGetResponse>> BuscarContaCorrentePorNumeroConta(string numerocontacorrente);
        Task<Result<ContaCorrenteGetResponse>> BuscarContaCorrentePorDocumento(string documento);
        Task<Result> CreditoContaCorrente(ContaCorrenteCreditoPostRequest request);

    }
}
