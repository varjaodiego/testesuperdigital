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

        public Result Adicionar(LancamentoTransferenciaPostRequest request)
        {
            
            var parametroValidos = ValidarParametroTransferencia(request);
            if (!parametroValidos)
                return Error(new MetaError(ListaErros.ParametrosNaoPodemSerVazio, StatusCode.Conflict));

            var contaOrigemExistente = ProcurarContaCorrente(request.contaOrigem);
            if (contaOrigemExistente == null)
                return Error(new MetaError(ListaErros.ContaOrigemNaoEncontrada, StatusCode.Conflict));

            var contaDestinoExistente = ProcurarContaCorrente(request.contaDestino);
            if (contaDestinoExistente == null)
                return Error(new MetaError(ListaErros.ContaDestinoNaoEncontrada, StatusCode.Conflict));

            if (contaOrigemExistente.saldo < request.valorTransacao)
                return Error(new MetaError(ListaErros.SaldoInsuficiente, StatusCode.Conflict));

            EfetivarLancamento(request);

            AtualizarSaldosContas(contaOrigemExistente, contaDestinoExistente, request.valorTransacao);

            return Success();

            
            
        }

        public void EfetivarLancamento(LancamentoTransferenciaPostRequest request)
        {
            var lancamento = EncapsularRequestParaModel(request);

            this.lancamentoRepository.Adicionar(lancamento);
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

        

        public void AtualizarSaldosContas(ContaCorrente contaOrigem, ContaCorrente contaDestino, decimal valorTransacao)
        {
            AtualizarSaldoOrigem(contaOrigem, valorTransacao);

            AtualizarSaldoDestino(contaDestino, valorTransacao);
        }

        public void AtualizarSaldoOrigem(ContaCorrente contaOrigem, decimal valorTransacao)
        {
            contaOrigem.saldo -= valorTransacao;
            
            AtualizarSaldo(contaOrigem);
        }

        public void AtualizarSaldoDestino(ContaCorrente contaDestino, decimal valorTransacao)
        {
            contaDestino.saldo += valorTransacao;
            AtualizarSaldo(contaDestino);
        }

        public void AtualizarSaldo(ContaCorrente conta)
        {
            this.contaCorrenteRepository.AtualizarSaldo(conta);
        }

        public ContaCorrente ProcurarContaCorrente(string numeroConta)
        {
            return this.contaCorrenteRepository.BuscarContaCorrentePorNumeroConta(numeroConta);
        }

        

    }
}
