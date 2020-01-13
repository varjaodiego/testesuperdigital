using System;

namespace superdigital.conta.data.Helpers
{
    public static class GerarNumeroContaCorrente
    {
        public static string GerarContaCorrente()
        {
            Random random = new Random();

            var inicioConta = random.Next(1, 1000);
            var finalConta = random.Next(200, 9999);
            var digitoConta = random.Next(0, 9);

            return string.Format("{0}{1}-{2}", inicioConta, finalConta, digitoConta);
        }
    }
}
