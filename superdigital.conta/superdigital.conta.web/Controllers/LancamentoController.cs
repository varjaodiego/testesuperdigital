using Microsoft.AspNetCore.Mvc;
using superdigital.conta.model;
using superdigital.conta.model.Contracts.Lancamento;
using superdigital.conta.model.Helpers;
using superdigital.conta.model.Interfaces;
using superdigital.conta.model.MetaErrors;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace superdigital.conta.web.Controllers
{
    
    [Route("/[controller]")]
    public class LancamentoController : Controller
    {
        /// <summary>
        /// classe controladora de cliente
        /// </summary>
        /// <param name="lancamentoService">instância de serviços de lançamento</param>
        public LancamentoController(ILancamentoService lancamentoService)
        {
            this.lancamentoService = lancamentoService;
        }

        private readonly ILancamentoService lancamentoService;

        /// <summary>
        /// Adiciona um lançamento na base de dados
        /// </summary>
        /// <param name="request">dados do lançamento a ser inserido</param>
        /// <returns>retorna um payload indicando se houve sucesso ou não</returns>
        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(MetaError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(MetaError))]
        public IActionResult AddLancamento([FromBody]LancamentoTransferenciaPostRequest request)
        {
            
            var result = this.lancamentoService.Adicionar(request);
            return HttpHelper.Convert(result);
            
        }
    }
}
