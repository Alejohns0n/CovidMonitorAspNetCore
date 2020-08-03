using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CovidMonitorAspNetCore.Models;
using static CovidMonitorAspNetCore.Models.PortalGeralApiResponse;
using static CovidMonitorAspNetCore.Models.DadosEstadoApiResponse;
using CovidMonitorAspNetCore.Code.JsonDownload;
using Newtonsoft.Json;
using CovidMonitorAspNetCore.Code.Ferramentas;

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
            Root dadosGeralResult = JsonConvert.DeserializeObject<Root>(jsonPortalGeral);

            var jsonPortalEstado = JsonRequest.PortalEstadoRequest();
            var dadosEstadosResult = JsonConvert.DeserializeObject<List<MyArray>>(jsonPortalEstado);

            ViewBag.CasosRecuperados = Ferramentas.FomataNumero(dadosGeralResult.confirmados.recuperados);
            ViewBag.CasosEmAcompanhamento = Ferramentas.FomataNumero(dadosGeralResult.confirmados.acompanhamento);
            ViewBag.CasosConfirmados = Ferramentas.FomataNumero(dadosGeralResult.confirmados.total);
            ViewBag.CasosNovos = Ferramentas.FomataNumero(dadosGeralResult.confirmados.novos.ToString());

            ViewBag.ObitosConfirmados = Ferramentas.FomataNumero(dadosGeralResult.obitos.total);
            ViewBag.ObitosNovos = Ferramentas.FomataNumero(dadosGeralResult.obitos.novos.ToString());

            ViewBag.DadosEstados = dadosEstadosResult;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}
