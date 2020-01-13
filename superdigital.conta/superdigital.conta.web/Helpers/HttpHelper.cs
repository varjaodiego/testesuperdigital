using Microsoft.AspNetCore.Mvc;
using superdigital.conta.model.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace superdigital.conta.model.Helpers
{
    
    public static class HttpHelper
    {
        /// <summary>
        /// Converte um Result para uma resposta HTTP.
        /// </summary>
        /// <param name="result">Dados do objeto de resultado.</param>
        /// <returns>Objeto Result em formato HTTP.</returns>
        public static IActionResult Convert(Result result)
        {
            if (result == null)
            {
                throw new ArgumentException("Result cannot be null.", "result");
            }

            if (result.Success)
            {
                return new OkResult();
            }

            if (result.MetaError.CodigoProtocoloHTTP == (int)HttpStatusCode.NotFound)
            {
                return new NotFoundObjectResult(result.MetaError.MensagemErro);
            }

            if (result.MetaError.CodigoProtocoloHTTP == (int)HttpStatusCode.BadRequest)
            {
                return new BadRequestObjectResult(result.MetaError.MensagemErro);
            }

            return new ObjectResult(result.MetaError.MensagemErro)
            {
                StatusCode = result.MetaError.CodigoProtocoloHTTP
            };
        }

        /// <summary>
        /// Converte um Result para uma resposta HTTP.
        /// </summary>
        /// <param name="result">Dados do objeto de resultado.</param>
        /// <typeparam name="T">Tipo de objeto embutido na resposta.</typeparam>
        /// <returns>Objeto Result em formato HTTP.</returns>
        public static IActionResult Convert<T>(Result<T> result)
        {
            if (result == null)
            {
                throw new ArgumentException("Result cannot be null.", "result");
            }

            if (result.Success)
            {
                return new OkObjectResult(result.Obj);
            }

            if (result.MetaError.CodigoProtocoloHTTP == (int)HttpStatusCode.NotFound)
            {
                return new NotFoundObjectResult(result.MetaError.MensagemErro);
            }

            if (result.MetaError.CodigoProtocoloHTTP == (int)HttpStatusCode.Conflict)
            {
                return new ObjectResult(result.MetaError.MensagemErro)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
            }

            return new BadRequestObjectResult(result.MetaError.MensagemErro);
        }
    }
    
}
