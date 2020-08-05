using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidMonitorAspNetCore.Code.Ferramentas;
using CovidMonitorAspNetCore.Code.JsonDownload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using static CovidMonitorAspNetCore.Code.Models.PortalCidadeApiResponse;

namespace CovidMonitorAspNetCore.Controllers
{
    public class BuscaCidadeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BuscarCidade(string nomeCidade)
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidade>>(dadosCidadeJsonResponse);

            ViewData["nomeCidade"] = nomeCidade;
            ViewBag.DadosCidades = dadosCidades;
            return View("Index");
        }
    }
}
