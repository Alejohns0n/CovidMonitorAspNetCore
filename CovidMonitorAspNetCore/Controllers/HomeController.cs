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

            ViewBag.CasosRecuperados = dadosGeralResult.confirmados.recuperados;
            ViewBag.CasosEmAcompanhamento = dadosGeralResult.confirmados.acompanhamento;
            ViewBag.CasosConfirmados = dadosGeralResult.confirmados.total;
            ViewBag.CasosNovos = dadosGeralResult.confirmados.novos;

            ViewBag.ObitosConfirmados = dadosGeralResult.obitos.total;
            ViewBag.ObitosNovos = dadosGeralResult.obitos.novos;

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
