using MongoDB.Bson;
using superdigital.conta.model.Enum;
using System;

namespace superdigital.conta.model
{
    public class ContaCorrente
    {
        public ObjectId id { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public string numeroConta { get; set; }
        public TipoPessoa tipoPessoa { get; set; }
        public TipoConta tipoConta { get; set; }
        public decimal saldo { get; set; }
        public DateTime dataCadastro { get; set; }
        public DateTime dataAtualizacao { get; set; }
                
    }
}
