using CovidMonitorAspNetCore.Code.Models;
using CovidMonitorAspNetCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CovidMonitorAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IJsonRequest _jsonRequest;
        private IFerramentas _ferramentas;


        public HomeController(ILogger<HomeController> logger, IJsonRequest jsonRequest, IFerramentas ferramentas)
        {
            _logger = logger;
            _jsonRequest = jsonRequest;
            _ferramentas = ferramentas;
        }

        public IActionResult Index()
        {
            var jsonPortalGeral = _jsonRequest.PortalGeralRequest();
            var dadosGeralResult = JsonConvert.DeserializeObject<PortalGeralApiResponse>(jsonPortalGeral);

            var jsonPortalEstado = _jsonRequest.PortalEstadoRequest();
            var dadosEstadosResult = JsonConvert.DeserializeObject<List<DadosEstadoApiResponse>>(jsonPortalEstado);

            for (int i = 0; i < dadosEstadosResult.Count; i++)
            {
                dadosEstadosResult[i].casosAcumulado = _ferramentas.FomataNumero(dadosEstadosResult[i].casosAcumulado);
                dadosEstadosResult[i].obitosAcumulado = _ferramentas.FomataNumero(dadosEstadosResult[i].obitosAcumulado);
            }

            ViewBag.CasosRecuperados = _ferramentas.FomataNumero(dadosGeralResult.confirmados.recuperados);
            ViewBag.CasosEmAcompanhamento = _ferramentas.FomataNumero(dadosGeralResult.confirmados.acompanhamento);
            ViewBag.CasosConfirmados = _ferramentas.FomataNumero(dadosGeralResult.confirmados.total);
            ViewBag.CasosNovos = _ferramentas.FomataNumero(dadosGeralResult.confirmados.novos.ToString());

            ViewBag.ObitosConfirmados = _ferramentas.FomataNumero(dadosGeralResult.obitos.total);
            ViewBag.ObitosNovos = _ferramentas.FomataNumero(dadosGeralResult.obitos.novos.ToString());

            ViewBag.DadosEstados = dadosEstadosResult;

            return View();
        }
    }
}
