using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Results;
using System.Threading.Tasks;

namespace superdigital.conta.model.Interfaces
{
    public interface ILancamentoService
    {
        Task<Result> Adicionar(LancamentoTransferenciaPostRequest lancamento);

    }
}
