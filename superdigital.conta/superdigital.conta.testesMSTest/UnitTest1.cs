using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using superdigital.conta.model.Constants;
using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;
using superdigital.conta.service;
using superdigital.conta.web.Controllers;

namespace superdigital.conta.testesMSTest
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        public UnitTest1()
        {
            request = Contrucao_LancamentoTransferenciaPostRequest();
        }

        private LancamentoTransferenciaPostRequest request;

        [TestMethod]
        public async System.Threading.Tasks.Task TestMethod1Async()
        {

            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ContaOrigemNaoEncontrada, StatusCode.Conflict)
            };

            var result = new Result();

            var service = new Mock<ILancamentoService>();

            request.contaOrigem = "8128465-2";
            service.Setup(set => set.Adicionar(request)).ReturnsAsync(expected);

            var controller = await service.Object.Adicionar(request);
            //var output = await controller.AddLancamento(request);

            Assert.AreEqual(result.MetaError.MensagemErro, ListaErros.ContaOrigemNaoEncontrada);
            Assert.AreEqual(expected.MetaError.MensagemErro, ListaErros.ContaOrigemNaoEncontrada);

        }
    }
}
