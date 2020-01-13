using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.model.Contracts.Lancamento
{
    public class LancamentoTransferenciaPostRequest
    {
        
        public string contaOrigem { get; set; }
        public string contaDestino { get; set; }
        public decimal valorTransacao { get; set; }
        
    }
}
