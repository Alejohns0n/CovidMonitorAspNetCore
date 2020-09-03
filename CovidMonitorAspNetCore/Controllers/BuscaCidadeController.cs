using System.Collections.Generic;
using System.Linq;
using CovidMonitorAspNetCore.Code.Ferramentas;
using CovidMonitorAspNetCore.Code.JsonDownload;
using CovidMonitorAspNetCore.Code.Models;
using CovidMonitorAspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CovidMonitorAspNetCore.Controllers
{
    public class BuscaCidadeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BuscaCidadeModel buscaCidadeModel)
        {
            var jsonCidadesDados = JsonRequest.PortalCidadeRequest();
            List<PortalCidadeApiResponse> dadosCidade = JsonConvert.DeserializeObject<List<PortalCidadeApiResponse>>(jsonCidadesDados);

            if (!string.IsNullOrEmpty(buscaCidadeModel.Cep))
            {
                var jsonCepDados = JsonRequest.CepRequest(buscaCidadeModel.Cep.Trim().Replace("-",""));
                DadosCepApiResponse dadosCep = JsonConvert.DeserializeObject<DadosCepApiResponse>(jsonCepDados);
                PortalCidadeApiResponse cidade = dadosCidade.Where(x => dadosCep.ibge.Contains(x.cod)).First();

                ViewData["nmeCidade"] = $"{cidade.nome}/{dadosCep.uf}";
                ViewData["cidadeCasos"] = Ferramentas.FomataNumero(cidade.casosAcumulado);
                ViewData["cidadeObitos"] = Ferramentas.FomataNumero(cidade.obitosAcumulado);

            }
            else if (!string.IsNullOrEmpty(buscaCidadeModel.NmeCidade))
            {
                var cidade = dadosCidade.Where(x => x.nome.ToLower().Trim() == buscaCidadeModel.NmeCidade.ToLower().Trim()).First();
                ViewData["nmeCidade"] = $"{cidade.nome}/{Ferramentas.BuscarUf(cidade)}";
                ViewData["cidadeCasos"] = Ferramentas.FomataNumero(cidade.casosAcumulado);
                ViewData["cidadeObitos"] = Ferramentas.FomataNumero(cidade.obitosAcumulado);
            }
            else
            {
                return Content("Preencha os campos corretamente!");
            }

            return View();
        }
    }
}
