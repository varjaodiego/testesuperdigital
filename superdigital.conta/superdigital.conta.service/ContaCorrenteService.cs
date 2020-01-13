using superdigital.conta.model;
using superdigital.conta.model.Constants;
using superdigital.conta.model.Contracts.ContaCorrente;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;
using superdigital.conta.service.Shared;
using System;


namespace superdigital.conta.service
{
    public class ContaCorrenteService : BaseService, IContaCorrenteService
    {
        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            this.contaCorrenteRepository = contaCorrenteRepository;
        }

        private readonly IContaCorrenteRepository contaCorrenteRepository;

        public Result AdicionarContaCorrente(ContaCorrentePostRequest request)
        {
            bool parametrosValidados = ValidacaoParametrosEntradaAdicionar(request);
            if (!parametrosValidados)
            {
                Error(new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict));
            }

            var conta = EncapsularRequestParaModel(request);

            this.contaCorrenteRepository.AdicionarContaCorrente(conta);

            return Success();
        }

        public ContaCorrente EncapsularRequestParaModel(ContaCorrentePostRequest request)
        {
            return new ContaCorrente()
            {
                dataCadastro = DateTime.Now,
                nome = request.nome,
                documento = request.documento,
                tipoConta = (TipoConta)request.tipoConta,
                tipoPessoa = (TipoPessoa)request.tipoPessoa
            };

        }

        public bool ValidacaoParametrosEntradaAdicionar(ContaCorrentePostRequest request)
        {
            var result = true;

            if (string.IsNullOrEmpty(request.nome) 
                || string.IsNullOrEmpty(request.documento)
                || !Enum.IsDefined(typeof(TipoPessoa), request.tipoConta) 
                || !Enum.IsDefined(typeof(TipoConta), request.tipoConta))
            {
                result = false;
            }

            return result;
                
        }

        public Result AtualizarSaldo(ContaCorrente conta)
        {
            try
            {
                this.contaCorrenteRepository.AtualizarSaldo(conta);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Error(new MetaError(ListaErros.ErroGenerico, StatusCode.InternalError));
            };
            
            return Success();
        }

        public Result<ContaCorrenteGetResponse> BuscarContaCorrentePorDocumento(string documento)
        {
            var conta = this.contaCorrenteRepository.BuscarContaCorrentePorDocumento(documento);

            var result = EncapsularModelParaGetResponse(conta);

            return Success(result);
        }

        public Result<ContaCorrenteGetResponse> BuscarContaCorrentePorNumeroConta(string numerocontacorrente)
        {
            var conta = this.contaCorrenteRepository.BuscarContaCorrentePorNumeroConta(numerocontacorrente);

            var result = EncapsularModelParaGetResponse(conta);

            return Success(result);
        }

        public ContaCorrenteGetResponse EncapsularModelParaGetResponse(ContaCorrente conta)
        {
            return new ContaCorrenteGetResponse()
            {
                documento = conta.documento,
                nome = conta.nome,
                numeroConta = conta.numeroConta,
                tipoConta = conta.tipoConta,
                tipoPessoa = conta.tipoPessoa,
                saldo = conta.saldo,
                dataAtualizacao = conta.dataAtualizacao,
                dataCadastro = conta.dataCadastro,
                id = conta.id
            };
        }

        public Result CreditoContaCorrente(ContaCorrenteCreditoPostRequest request)
        {
            var isParameterValid = ValidarParametroCredito(request);
            if (!isParameterValid)
                return Error(new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict));

            var contaCredito = ProcurarContaCorrente(request.numeroConta);
            if (contaCredito == null)
            {
                return Error(new MetaError(ListaErros.ContaDestinoNaoEncontrada, StatusCode.Conflict));
            }

            contaCredito.saldo += request.valorCredito;
            var result = AtualizarSaldo(contaCredito);

            return Success(result);


        }
        public bool ValidarParametroCredito(ContaCorrenteCreditoPostRequest request)
        {
            var result = true;
            if (request.valorCredito <= decimal.Zero || string.IsNullOrEmpty(request.numeroConta))
                result = false;

            return result;
        }

        public ContaCorrente ProcurarContaCorrente(string numerocontacorrente)
        {
            return this.contaCorrenteRepository.BuscarContaCorrentePorNumeroConta(numerocontacorrente);

             
        }
    }
}
