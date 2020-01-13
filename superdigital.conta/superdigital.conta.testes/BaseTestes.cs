using Moq;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.Contracts.ContaCorrente;
using superdigital.conta.model.Contracts.Lancamento;
using System;
using System.Collections.Generic;
using System.Text;


namespace superdigital.conta.testes
{
    public class BaseTestes
    {
        
        protected LancamentoTransferenciaPostRequest Contrucao_LancamentoTransferenciaPostRequest()
        {
            return new LancamentoTransferenciaPostRequest()
            {
                contaDestino = "8121665-1",
                contaOrigem = "4025168-2",
                valorTransacao = 100
            };
        }

        protected ContaCorrentePostRequest Contrucao_ContaCorrentePostRequest()
        {
            return new ContaCorrentePostRequest()
            {
                documento = "21189153068",
                nome = "Teste Mock",
                tipoConta = 1,
                tipoPessoa = 1
            };
        }

        protected ContaCorrenteCreditoPostRequest Construcao_ContaCorrenteCreditoPostRequest()
        {
            return new ContaCorrenteCreditoPostRequest()
            {
                numeroConta = "4025168-2",
                valorCredito = 1000
            };
        }
    }
}
