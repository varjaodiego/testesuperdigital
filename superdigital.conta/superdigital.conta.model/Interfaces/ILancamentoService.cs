using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Results;

namespace superdigital.conta.model.Interfaces
{
    public interface ILancamentoService
    {
        Result Adicionar(LancamentoTransferenciaPostRequest lancamento);

    }
}
