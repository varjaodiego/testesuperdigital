using superdigital.conta.model.Contracts.ContaCorrente;
using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.testes
{
    public class ContaTestes : BaseTestes
    {
        public ContaTestes()
        {
            requestConta = Contrucao_ContaCorrentePostRequest();
            requestCredito = Construcao_ContaCorrenteCreditoPostRequest();
        }

        private ContaCorrentePostRequest requestConta;
        private ContaCorrenteCreditoPostRequest requestCredito;


    }
}
