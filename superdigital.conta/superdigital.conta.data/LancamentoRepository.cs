using superdigital.conta.data.Helpers;
using superdigital.conta.model;
using superdigital.conta.model.Interfaces;
using System;

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

        public void Adicionar(Lancamento lancamento)
        {
            lancamento.id = GerarID.GerarObjectID();
            lancamento.idenficadorTransacao = Guid.NewGuid().ToString();
            
            var database = this.context.getDatabase();
            database.GetCollection<Lancamento>(this.collectionName).InsertOne(lancamento);
        }
    }
}
