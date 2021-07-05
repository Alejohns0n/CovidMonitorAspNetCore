using CovidMonitorAspNetCore.Code.Models;
using CovidMonitorAspNetCore.Interfaces;
using CovidMonitorAspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CovidMonitorAspNetCore.Controllers
{
    [Route("buscacidade")]
    public class BuscaCidadeController : Controller
    {
        private IJsonRequest _jsonRequest;
        private IFerramentas _ferramentas;

        public BuscaCidadeController(IJsonRequest jsonRequest, IFerramentas ferramentas)
        {
            _jsonRequest = jsonRequest;
            _ferramentas = ferramentas;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BuscaCidadeModel buscaCidadeModel)
        {
            MunicipiosServicosDadosApiResponse municipiosServicosDadosApi = _ferramentas.BuscarCidadeExata(buscaCidadeModel.NmeCidade);
            if(municipiosServicosDadosApi == null)
            {
                ViewData["error"] = "Cidade não foi encontrada!";
                return View();
            }

            List<PortalCidadeApiResponse> dadosCidade =
                JsonConvert.DeserializeObject<List<PortalCidadeApiResponse>>(_jsonRequest.PortalCidadeRequest());

            PortalCidadeApiResponse cidade;
            try
            {
                cidade = dadosCidade.Where(x => municipiosServicosDadosApi.id.Contains(x.cod)).FirstOrDefault();
            }
            catch
            {
                ViewData["error"] = "Cidade não foi encontrada!";
                return View();
            }
            if (cidade == null)
            {
                ViewData["error"] = "Cidade não foi encontrada!";
                return View();
            }
            ViewData["nmeCidade"] = $"{municipiosServicosDadosApi.nome}/{municipiosServicosDadosApi.microrregiao.mesorregiao.UF.sigla}";
            ViewData["cidadeCasos"] = _ferramentas.FomataNumero(cidade.casosAcumulado);
            ViewData["cidadeObitos"] = _ferramentas.FomataNumero(cidade.obitosAcumulado);

            return View();
        }
    }
}
