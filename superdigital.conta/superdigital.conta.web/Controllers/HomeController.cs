using Microsoft.AspNetCore.Mvc;

namespace superdigital.conta.web.Controllers
{
    /// <summary>
    /// Controller de home
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Rota que redireciona para a documentação do projeto
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
