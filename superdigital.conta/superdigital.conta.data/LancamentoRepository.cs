using superdigital.conta.data.Helpers;
using superdigital.conta.model;
using superdigital.conta.model.Interfaces;
using System;
using System.Threading.Tasks;

namespace superdigital.conta.data
{
    public class LancamentoRepository : ILancamentoRepository
    {
        public LancamentoRepository(MongoContext context)
        {
            this.context = context;
        }

        readonly MongoContext context;
        string collectionName = "lancamentos";

        public async Task Adicionar(Lancamento lancamento)
        {
            lancamento.id = GerarID.GerarObjectID();
            lancamento.idenficadorTransacao = Guid.NewGuid().ToString();
            
            var database = this.context.getDatabase();
            await database.GetCollection<Lancamento>(this.collectionName).InsertOneAsync(lancamento);
        }
    }
}
