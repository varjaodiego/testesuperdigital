using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.model
{
    public class Lancamento
    {
        public ObjectId id { get; set; }
        public string idenficadorTransacao { get; set; }
        public string contaOrigem { get; set; }
        public string contaDestino { get; set; }
        public decimal valorTransacao { get; set; }
        public DateTime DataLancamento { get; set; }

    }
}
