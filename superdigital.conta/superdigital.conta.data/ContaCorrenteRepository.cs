using MongoDB.Bson;
using MongoDB.Driver;
using superdigital.conta.data.Helpers;
using superdigital.conta.model;
using superdigital.conta.model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace superdigital.conta.data
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        public ContaCorrenteRepository(MongoContext context)
        {
            this.context = context;
        }

        readonly MongoContext context;
        string collectionName = "contaCorrente";

        public async Task AdicionarContaCorrente(ContaCorrente conta)
        {
            conta.id = GerarID.GerarObjectID();
            conta.dataCadastro = DateTime.Now;
            conta.numeroConta = GerarNumeroContaCorrente.GerarContaCorrente();
            conta.saldo = decimal.Zero;

            var database = this.context.getDatabase();
            await database.GetCollection<ContaCorrente>(collectionName).InsertOneAsync(conta);
        }

        public async Task AtualizarSaldo(ContaCorrente conta)
        {
            conta.dataAtualizacao = DateTime.Now;

            var filter = Builders<ContaCorrente>.Filter.Eq("numeroConta", conta.numeroConta);
            var update = Builders<ContaCorrente>.Update
                .Set(upd => upd.saldo, conta.saldo)
                .Set(upd => upd.dataAtualizacao, conta.dataAtualizacao);

            var database = this.context.getDatabase();
            await database.GetCollection<ContaCorrente>(collectionName).UpdateOneAsync(filter, update);
        }

        public async Task<ContaCorrente> BuscarContaCorrentePorDocumento(string documento)
        {
            var database = this.context.getDatabase();
            var filter = Builders<ContaCorrente>.Filter.Eq("documento", documento);
            var result = await database.GetCollection<ContaCorrente>(collectionName).FindAsync(filter).Result.FirstOrDefaultAsync();
            return result;
        }

        public async Task<ContaCorrente> BuscarContaCorrentePorNumeroConta(string numerocontacorrente)
        {
            var database = this.context.getDatabase();
            var filter = Builders<ContaCorrente>.Filter.Eq("numeroConta", numerocontacorrente);
            var result = await database.GetCollection<ContaCorrente>(collectionName).FindAsync(filter).Result.FirstOrDefaultAsync();
            return result;
        }
    }
}
