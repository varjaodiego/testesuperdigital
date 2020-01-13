using Moq;
using superdigital.conta.model.Constants;
using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.Results;
using System;
using Xunit;

namespace superdigital.conta.testes
{
    public class LancamentoTestes : BaseTestes
    {
        private LancamentoTransferenciaPostRequest request;

        public LancamentoTestes()
        {
            request = Contrucao_LancamentoTransferenciaPostRequest();
        }
        
        [Fact]
        public void Lancamento_Adicionar_Success()
        {
            var result = new Result();

            var service = new Mock<ILancamentoService>();
            service.Setup(set => set.Adicionar(request)).Returns(result);
            
            Assert.True(result.Success);
        }

        [Fact]
        public void Lancamento_Adicionar_Fail_Conta_Origem_Inexistente()
        {
            var result = new Result();
            var service = new Mock<ILancamentoService>();

            request.contaOrigem = "8128465-2";
            service.Setup(set => set.Adicionar(request)).Returns(result);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ContaOrigemNaoEncontrada);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public void Lancamento_Adicionar_Fail_Conta_Origem_Vazia()
        {
            var result = new Result();
            var service = new Mock<ILancamentoService>();

            request.contaOrigem = "";
            service.Setup(set => set.Adicionar(request)).Returns(result);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public void Lancamento_Adicionar_Fail_Conta_Destino_Vazia()
        {
            var result = new Result();
            var service = new Mock<ILancamentoService>();

            request.contaDestino = "";
            service.Setup(set => set.Adicionar(request)).Returns(result);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public void Lancamento_Adicionar_Fail_Conta_Destino_Inexistente()
        {
            var result = new Result();
            var service = new Mock<ILancamentoService>();

            request.contaDestino = "2128465-2";
            service.Setup(set => set.Adicionar(request)).Returns(result);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ContaDestinoNaoEncontrada);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public void Lancamento_Adicionar_Fail_Valor_Transferencia_Zero()
        {
            var result = new Result();
            var service = new Mock<ILancamentoService>();

            request.valorTransacao = 0;
            service.Setup(set => set.Adicionar(request)).Returns(result);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.SaldoInsuficiente);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }
    }
}
