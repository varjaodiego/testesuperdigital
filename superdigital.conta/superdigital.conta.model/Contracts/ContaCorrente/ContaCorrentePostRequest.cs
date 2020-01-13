using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.model.Contracts.ContaCorrente
{
    public class ContaCorrentePostRequest
    {
        public string nome { get; set; }
        public string documento { get; set; }
        public int tipoPessoa { get; set; }
        public int tipoConta { get; set; }
        
    }
}
