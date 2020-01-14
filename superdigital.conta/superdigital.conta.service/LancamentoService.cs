using superdigital.conta.model;
using superdigital.conta.model.Interfaces;
using superdigital.conta.service.Shared;
using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Enum;
using superdigital.conta.model.Constants;
using System.Threading.Tasks;

namespace superdigital.conta.service
{
    public class LancamentoService : BaseService, ILancamentoService
    {
        public LancamentoService(ILancamentoRepository lancamentoRepository, IContaCorrenteRepository contaCorrenteRepository)
        {
            this.lancamentoRepository = lancamentoRepository;
            this.contaCorrenteRepository = contaCorrenteRepository;
        }

        private readonly ILancamentoRepository lancamentoRepository;
        private readonly IContaCorrenteRepository contaCorrenteRepository;

        public async Task<Result> Adicionar(LancamentoTransferenciaPostRequest request)
        {
            
            var parametroValidos = ValidarParametroTransferencia(request);
            if (!parametroValidos)
                return Error(new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict));

            if (await OrigemDestinoIguais(request))
                return Error(new MetaError(ListaErros.OrigemDestinoNaoPodemSerIguais, StatusCode.Conflict));
            

            var contaOrigemExistente = await ProcurarContaCorrente(request.contaOrigem);
            if (contaOrigemExistente == null)
                return Error(new MetaError(ListaErros.ContaOrigemNaoEncontrada, StatusCode.Conflict));

            var contaDestinoExistente = await ProcurarContaCorrente(request.contaDestino);
            if (contaDestinoExistente == null)
                return Error(new MetaError(ListaErros.ContaDestinoNaoEncontrada, StatusCode.Conflict));

            if (contaOrigemExistente.saldo < request.valorTransacao)
                return Error(new MetaError(ListaErros.SaldoInsuficiente, StatusCode.Conflict));

            await EfetivarLancamento(request);

            await AtualizarSaldosContas(contaOrigemExistente, contaDestinoExistente, request.valorTransacao);

            return Success();

        }

        public async Task<bool> OrigemDestinoIguais(LancamentoTransferenciaPostRequest request)
        {
            var result = true;
            if (request.contaDestino == request.contaOrigem)
                result = false;

            return result;
        }

        public async Task EfetivarLancamento(LancamentoTransferenciaPostRequest request)
        {
            var lancamento = EncapsularRequestParaModel(request);

            await this.lancamentoRepository.Adicionar(lancamento);
        }

        public Lancamento EncapsularRequestParaModel(LancamentoTransferenciaPostRequest request)
        {
            return new Lancamento()
            {
                contaDestino = request.contaDestino,
                contaOrigem = request.contaOrigem,
                valorTransacao = request.valorTransacao,
                DataLancamento = DateTime.Now
            };
        }

        public bool ValidarParametroTransferencia(LancamentoTransferenciaPostRequest lancamento)
        {
            var result = true;
            if (lancamento.valorTransacao <= 0
                       || string.IsNullOrEmpty(lancamento.contaOrigem)
                       || string.IsNullOrEmpty(lancamento.contaDestino))
                result = false;

            return result;
        }

        

        public async Task AtualizarSaldosContas(ContaCorrente contaOrigem, ContaCorrente contaDestino, decimal valorTransacao)
        {
            await AtualizarSaldoOrigem(contaOrigem, valorTransacao);

            await AtualizarSaldoDestino(contaDestino, valorTransacao);
        }

        public async Task AtualizarSaldoOrigem(ContaCorrente contaOrigem, decimal valorTransacao)
        {
            contaOrigem.saldo -= valorTransacao;
            
            await AtualizarSaldo(contaOrigem);
        }

        public async Task AtualizarSaldoDestino(ContaCorrente contaDestino, decimal valorTransacao)
        {
            contaDestino.saldo += valorTransacao;
            await AtualizarSaldo(contaDestino);
        }

        public async Task AtualizarSaldo(ContaCorrente conta)
        {
            this.contaCorrenteRepository.AtualizarSaldo(conta);
        }

        public async Task<ContaCorrente> ProcurarContaCorrente(string numeroConta)
        {
            return await this.contaCorrenteRepository.BuscarContaCorrentePorNumeroConta(numeroConta);
        }

        

    }
}
