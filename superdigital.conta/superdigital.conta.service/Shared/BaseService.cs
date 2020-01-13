using superdigital.conta.model.MetaErrors;
using superdigital.conta.model.Results;

namespace superdigital.conta.service.Shared
{
    public class BaseService
    {

        public Result Success()
        {
            return new Result();
        }

        /// <summary>
        /// Resposta de sucesso com objeto de retorno.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno.</typeparam>
        /// <param name="obj">Objeto de retorno.</param>
        /// <returns>Resposta de sucesso com objecto de retorno.</returns>
        public Result<T> Success<T>(T obj)
        {
            return new Result<T>() { Obj = obj };
        }

        /// <summary>
        /// Resposta de erro.
        /// </summary>
        /// <param name="metaError">Dados do erro ocorrido.</param>
        /// <returns>Resposta de erro.</returns>
        public Result Error(MetaError metaError)
        {
            return new Result() { MetaError = metaError };
        }

        /// <summary>
        /// Resposta de erro.
        /// </summary>
        /// <param name="metaError">Dados do erro ocorrido.</param>
        /// <typeparam name="T">Tipo do objeto de retorno.</typeparam>
        /// <returns>Resposta de erro.</returns>
        public Result<T> Error<T>(MetaError metaError)
        {
            return new Result<T>() { MetaError = metaError, Obj = default(T) };
        }
    }
}
