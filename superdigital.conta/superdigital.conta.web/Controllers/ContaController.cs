using Microsoft.AspNetCore.Mvc;
using superdigital.conta.model.Contracts.ContaCorrente;
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
    public class ContaController : Controller
    {
        public ContaController(IContaCorrenteService contaCorrenteService)
        {
            this.contaCorrenteService = contaCorrenteService;
        }

        private readonly IContaCorrenteService contaCorrenteService;


        /// <summary>
        /// Add uma conta corrente
        /// </summary>
        /// <returns>retorna apenas status code</returns>
        [HttpPost]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(MetaError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(MetaError))]
        public async Task<IActionResult> AddContaCorrente([FromBody] ContaCorrentePostRequest request )
        {
            var cliente = await this.contaCorrenteService.AdicionarContaCorrente(request);

            return HttpHelper.Convert(cliente);

        }

        /// <summary>
        /// Add saldo há uma conta corrente
        /// </summary>
        /// <returns>retorna apenas status code</returns>
        [HttpPost]
        [Route("credito")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(MetaError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(MetaError))]
        public async Task<IActionResult> AddCreditoContaCorrente([FromBody] ContaCorrenteCreditoPostRequest request)
        {
            var cliente = await this.contaCorrenteService.CreditoContaCorrente(request);

            return HttpHelper.Convert(cliente);

        }

        /// <summary>
        /// Retorna uma conta corrente atráves do documento informado
        /// </summary>
        /// <param name="documento">documento</param>
        /// <returns>retorna uma conta corrente atráves do documento informado</returns>
        [HttpGet]
        [Route("documento/{documento}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(MetaError))]
        public async Task<IActionResult> GetCorrentistaPorDocumento(string documento)
        {
            var cliente = await this.contaCorrenteService.BuscarContaCorrentePorDocumento(documento);

            return HttpHelper.Convert(cliente);
            
        }

        /// <summary>
        /// Retorna uma conta corrente atráves do numero da conta informada
        /// </summary>
        /// <param name="contacorrente">numero conta corrente</param>
        /// <returns>retorna uma conta corrente atráves do numero da conta corrente informada</returns>
        [HttpGet]
        [Route("numeroconta/{contacorrente}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(MetaError))]
        public async Task<IActionResult> GetCorrentistaPorNumeroConta(string contacorrente)
        {
            var cliente = await this.contaCorrenteService.BuscarContaCorrentePorNumeroConta(contacorrente);

            return HttpHelper.Convert(cliente);

        }
    }
}
