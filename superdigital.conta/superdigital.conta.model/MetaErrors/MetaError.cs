using System;
using System.Collections.Generic;
using System.Text;

namespace superdigital.conta.model.MetaErrors
{
    public class MetaError
    {
        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="protocolCode"></param>
        public MetaError(string error, int? protocolCode)
        {
            MensagemErro = error;
            CodigoProtocoloHTTP = protocolCode;
        }

        /// <summary>
        /// Mensagem de Erro.
        /// </summary>
        public string MensagemErro { get; set; }

        /// <summary>
        /// Código do protocolo.
        /// </summary>
        public int? CodigoProtocoloHTTP { get; set; }
    }
}
