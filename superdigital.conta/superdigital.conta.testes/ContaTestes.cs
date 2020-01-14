using Moq;
using superdigital.conta.model.Constants;
using superdigital.conta.model.Contracts.ContaCorrente;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;
using System.Threading.Tasks;
using Xunit;

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

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Success()
        {
            var expected = new Result();

            var service = new Mock<IContaCorrenteService>();

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Parametros_Incorretos_Nome()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            requestConta.nome = string.Empty;

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Parametros_Incorretos_Documento()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            requestConta.documento = string.Empty;

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Parametros_Incorretos_TipoConta()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            requestConta.tipoConta = 10;

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Parametros_Incorretos_TipoPessoa()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            requestConta.tipoPessoa = 7;

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Documento_Invalido_CPF()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.DocumentoInvalido, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            requestConta.documento = "03829792018";

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.DocumentoInvalido);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Documento_Invalido_CNPJ()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.DocumentoInvalido, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            requestConta.documento = "38210019792018";

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.DocumentoInvalido);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_AdicionarContaCorrente_Fail_Documento_Ja_Cadastrado()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.DocumentoJaCadastrado, StatusCode.Conflict)
            };

            var service = new Mock<IContaCorrenteService>();

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.DocumentoJaCadastrado);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_CreditoContaCorrente_Fail_ContaCorrente_Vazio()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            requestCredito.numeroConta = string.Empty;

            var service = new Mock<IContaCorrenteService>();

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        [Fact]
        public async Task ContaCorrente_CreditoContaCorrente_Fail_Valor_Credito_Zero()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict)
            };

            requestCredito.valorCredito = decimal.Zero;

            var service = new Mock<IContaCorrenteService>();

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ParametrosNaoPodemSerVazio);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

        public async Task ContaCorrente_CreditoContaCorrente_Fail_ContaCorrente_Nao_Existe()
        {
            var expected = new Result()
            {
                MetaError = new MetaError(ListaErros.ContaDestinoNaoEncontrada, StatusCode.Conflict)
            };

            requestCredito.numeroConta = "424234234-1";

            var service = new Mock<IContaCorrenteService>();

            service.Setup(set => set.AdicionarContaCorrente(requestConta)).ReturnsAsync(expected);

            var result = await service.Object.AdicionarContaCorrente(requestConta);

            Assert.False(result.Success);
            Assert.Equal(result.MetaError.MensagemErro, ListaErros.ContaDestinoNaoEncontrada);
            Assert.Equal(result.MetaError.CodigoProtocoloHTTP, StatusCode.Conflict);
        }

    }
}
