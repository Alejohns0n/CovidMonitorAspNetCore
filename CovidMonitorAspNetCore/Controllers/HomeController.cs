using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CovidMonitorAspNetCore.Code.JsonDownload;
using Newtonsoft.Json;
using CovidMonitorAspNetCore.Code.Ferramentas;
using CovidMonitorAspNetCore.Code.Models;

namespace CovidMonitorAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var jsonPortalGeral = JsonRequest.PortalGeralRequest();
            var dadosGeralResult = JsonConvert.DeserializeObject<PortalGeralApiResponse>(jsonPortalGeral);

            var jsonPortalEstado = JsonRequest.PortalEstadoRequest();
            var dadosEstadosResult = JsonConvert.DeserializeObject<List<DadosEstadoApiResponse>>(jsonPortalEstado);

            ViewBag.CasosRecuperados = Ferramentas.FomataNumero(dadosGeralResult.confirmados.recuperados);
            ViewBag.CasosEmAcompanhamento = Ferramentas.FomataNumero(dadosGeralResult.confirmados.acompanhamento);
            ViewBag.CasosConfirmados = Ferramentas.FomataNumero(dadosGeralResult.confirmados.total);
            ViewBag.CasosNovos = Ferramentas.FomataNumero(dadosGeralResult.confirmados.novos.ToString());

            ViewBag.ObitosConfirmados = Ferramentas.FomataNumero(dadosGeralResult.obitos.total);
            ViewBag.ObitosNovos = Ferramentas.FomataNumero(dadosGeralResult.obitos.novos.ToString());

            ViewBag.DadosEstados =  dadosEstadosResult;

            return View();
        }
    }
}
