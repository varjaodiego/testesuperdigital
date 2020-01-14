using superdigital.conta.model;
using superdigital.conta.model.Constants;
using superdigital.conta.model.Contracts.ContaCorrente;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;
using superdigital.conta.service.Helpers;
using superdigital.conta.service.Shared;
using System;
using System.Threading.Tasks;

namespace superdigital.conta.service
{
    public class ContaCorrenteService : BaseService, IContaCorrenteService
    {
        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            this.contaCorrenteRepository = contaCorrenteRepository;
        }

        private readonly IContaCorrenteRepository contaCorrenteRepository;

        public async Task<Result> AdicionarContaCorrente(ContaCorrentePostRequest request)
        {
            bool parametrosValidados = await ValidacaoParametrosEntradaAdicionar(request);
            if (!parametrosValidados)
                return Error(new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict));

            var documentoValido = await DocumentoValido(request.documento);
            if (documentoValido)
                return Error(new MetaError(ListaErros.DocumentoInvalido, StatusCode.Conflict));

            var documentoExistente = await BuscarContaCorrentePorDocumento(request.documento);
            if (documentoExistente != null)
                return Error(new MetaError(ListaErros.DocumentoJaCadastrado, StatusCode.Conflict));

            var conta = EncapsularRequestParaModel(request);

            await this.contaCorrenteRepository.AdicionarContaCorrente(conta);

            return Success();
        }

        public async Task<bool> DocumentoValido(string documento)
        {
            return documento.Length == 11 ? documento.IsCpf() : documento.IsCnpj();
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

        public async Task<bool> ValidacaoParametrosEntradaAdicionar(ContaCorrentePostRequest request)
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

        public async Task<Result> AtualizarSaldo(ContaCorrente conta)
        {
            try
            {
                await this.contaCorrenteRepository.AtualizarSaldo(conta);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Error(new MetaError(ListaErros.ErroGenerico, StatusCode.InternalError));
            };
            
            return Success();
        }

        public async Task<Result<ContaCorrenteGetResponse>> BuscarContaCorrentePorDocumento(string documento)
        {
            var conta = await this.contaCorrenteRepository.BuscarContaCorrentePorDocumento(documento);

            var result = EncapsularModelParaGetResponse(conta);

            return Success(result);
        }

        public async Task<Result<ContaCorrenteGetResponse>> BuscarContaCorrentePorNumeroConta(string numerocontacorrente)
        {
            var conta = await this.contaCorrenteRepository.BuscarContaCorrentePorNumeroConta(numerocontacorrente);

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

        public async Task<Result> CreditoContaCorrente(ContaCorrenteCreditoPostRequest request)
        {
            var isParameterValid = await ValidarParametroCredito(request);
            if (!isParameterValid)
                return Error(new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict));

            var contaCredito = await ProcurarContaCorrente(request.numeroConta);
            if (contaCredito == null)
                return Error(new MetaError(ListaErros.ContaDestinoNaoEncontrada, StatusCode.Conflict));
            
            contaCredito.saldo += request.valorCredito;
            var result = await AtualizarSaldo(contaCredito);

            return Success(result);

        }
        public async Task<bool> ValidarParametroCredito(ContaCorrenteCreditoPostRequest request)
        {
            var result = true;
            if (request.valorCredito <= decimal.Zero || string.IsNullOrEmpty(request.numeroConta))
                result = false;

            return result;
        }

        public async Task<ContaCorrente> ProcurarContaCorrente(string numerocontacorrente)
        {
            return await this.contaCorrenteRepository.BuscarContaCorrentePorNumeroConta(numerocontacorrente);

             
        }
    }
}
