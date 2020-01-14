using Moq;
using superdigital.conta.model.Constants;
using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;
using superdigital.conta.web.Controllers;
using System;
using System.Threading.Tasks;
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
        public async Task Lancamento_Adicionar_Success()
        {
            var expected = new Result();

            var service = new Mock<ILancamentoService>();

            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var result = await service.Object.Adicionar(request);

            Assert.True(result.Success);
        }
        
        [Fact]
        public async Task Lancamento_Adicionar_Fail_Conta_Origem_Inexistente()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ContaOrigemNaoEncontrada, StatusCode.Conflict)
            };

            var service = new Mock<ILancamentoService>();

            request.contaOrigem = "8128465-2";
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);
                
            var result = await service.Object.Adicionar(request);
            

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ContaOrigemNaoEncontrada);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }
        
        [Fact]
        public async Task Lancamento_Adicionar_Fail_Conta_Origem_Vazia()
        {

            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            
            var service = new Mock<ILancamentoService>();

            request.contaOrigem = "";
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var result = await service.Object.Adicionar(request);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task Lancamento_Adicionar_Fail_Conta_Destino_Vazia()
        {

            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            var service = new Mock<ILancamentoService>();

            request.contaDestino = "";
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var result = await service.Object.Adicionar(request);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);

        }

        [Fact]
        public async Task Lancamento_Adicionar_Fail_Conta_Destino_Inexistente()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ContaDestinoNaoEncontrada, StatusCode.Conflict)
            };

            var service = new Mock<ILancamentoService>();

            request.contaDestino = "2128465-2";
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var result = await service.Object.Adicionar(request);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ContaDestinoNaoEncontrada);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);

        }

        [Fact]
        public async Task Lancamento_Adicionar_Fail_Valor_Transferencia_Zero()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.SaldoInsuficiente, StatusCode.Conflict)
            };

            var service = new Mock<ILancamentoService>();

            request.valorTransacao = 0;
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var result = await service.Object.Adicionar(request);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.SaldoInsuficiente);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        public async Task Lancamento_Adicionar_Fail_ContaOrigem_ContaDestino_Iguais()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.OrigemDestinoNaoPodemSerIguais, StatusCode.Conflict)
            };

            var service = new Mock<ILancamentoService>();

            request.contaDestino = "2128465-2";
            request.contaOrigem = "2128465-2";
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var result = await service.Object.Adicionar(request);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.OrigemDestinoNaoPodemSerIguais);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

    }
}
