
using superdigital.conta.model.Contracts.ContaCorrente;
using superdigital.conta.model.Results;

namespace superdigital.conta.model.Interfaces
{
    public interface IContaCorrenteService
    {
        Result AdicionarContaCorrente(ContaCorrentePostRequest conta);
        Result AtualizarSaldo(ContaCorrente conta);
        Result<ContaCorrenteGetResponse> BuscarContaCorrentePorNumeroConta(string numerocontacorrente);
        Result<ContaCorrenteGetResponse> BuscarContaCorrentePorDocumento(string documento);
        Result CreditoContaCorrente(ContaCorrenteCreditoPostRequest request);

    }
}
