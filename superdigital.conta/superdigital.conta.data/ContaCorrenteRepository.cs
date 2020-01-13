using MongoDB.Bson;
using MongoDB.Driver;
using superdigital.conta.data.Helpers;
using superdigital.conta.model;
using superdigital.conta.model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void AdicionarContaCorrente(ContaCorrente conta)
        {
            conta.id = GerarID.GerarObjectID();
            conta.dataCadastro = DateTime.Now;
            conta.numeroConta = GerarNumeroContaCorrente.GerarContaCorrente();
            conta.saldo = decimal.Zero;

            var database = this.context.getDatabase();
            database.GetCollection<ContaCorrente>(this.collectionName).InsertOne(conta);
        }

        public void AtualizarSaldo(ContaCorrente conta)
        {
            conta.dataAtualizacao = DateTime.Now;

            var filter = Builders<ContaCorrente>.Filter.Eq("numeroConta", conta.numeroConta);
            var update = Builders<ContaCorrente>.Update
                .Set(upd => upd.saldo, conta.saldo)
                .Set(upd => upd.dataAtualizacao, conta.dataAtualizacao);

            var database = this.context.getDatabase();
            database.GetCollection<ContaCorrente>(this.collectionName).UpdateOne(filter, update);
        }

        public ContaCorrente BuscarContaCorrentePorDocumento(string documento)
        {
            var database = this.context.getDatabase();
            var filter = Builders<ContaCorrente>.Filter.Eq("documento", documento);
            return database.GetCollection<ContaCorrente>(this.collectionName).FindSync(filter).FirstOrDefault();
        }

        public ContaCorrente BuscarContaCorrentePorNumeroConta(string numerocontacorrente)
        {
            var database = this.context.getDatabase();
            var filter = Builders<ContaCorrente>.Filter.Eq("numeroConta", numerocontacorrente);
            return database.GetCollection<ContaCorrente>(this.collectionName).FindSync(filter).FirstOrDefault();
        }
    }
}
