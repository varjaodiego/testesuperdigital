using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.model.Contracts.ContaCorrente
{
    public class ContaCorrenteCreditoPostRequest
    {
        public string numeroConta { get; set; }
        public decimal valorCredito { get; set; }
    }
}
