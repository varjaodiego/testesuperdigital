
using System.Threading.Tasks;

namespace superdigital.conta.model.Interfaces
{
    public interface ILancamentoRepository
    {
        Task Adicionar(Lancamento lancamento);
    }
}
