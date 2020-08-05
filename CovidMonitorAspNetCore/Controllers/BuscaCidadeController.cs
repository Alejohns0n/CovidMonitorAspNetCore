using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidMonitorAspNetCore.Code.Ferramentas;
using CovidMonitorAspNetCore.Code.JsonDownload;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static CovidMonitorAspNetCore.Code.Models.PortalCidadeApiResponse;

namespace CovidMonitorAspNetCore.Controllers
{
    public class BuscaCidadeController : Controller
    {
        public IActionResult Index()
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidade>>(dadosCidadeJsonResponse);
            ViewBag.DadosCidades = dadosCidades;

            return View();
        }
    }
}
