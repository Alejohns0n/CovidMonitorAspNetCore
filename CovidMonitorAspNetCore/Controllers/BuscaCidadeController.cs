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
            var jsonCidadesDados = _jsonRequest.PortalCidadeRequest();
            List<PortalCidadeApiResponse> dadosCidade = JsonConvert.DeserializeObject<List<PortalCidadeApiResponse>>(jsonCidadesDados);

            if (!string.IsNullOrEmpty(buscaCidadeModel.Cep))
            {
                if (Regex.IsMatch(buscaCidadeModel.Cep, @"[aA-zA]"))
                {
                    ViewData["error"] = "Caracteres inválidos no CEP!";
                    return View();
                }
                var jsonCepDados = _jsonRequest.CepRequest(buscaCidadeModel.Cep.Trim().Replace("-", ""));
                if (jsonCepDados.Contains("erro"))
                {
                    ViewData["error"] = "Cep não encontrado";
                    return View();
                }
                DadosCepApiResponse dadosCep = JsonConvert.DeserializeObject<DadosCepApiResponse>(jsonCepDados);
                PortalCidadeApiResponse cidade = dadosCidade.Where(x => dadosCep.ibge.Contains(x.cod)).First();

                ViewData["nmeCidade"] = $"{cidade.nome}/{dadosCep.uf}";
                ViewData["cidadeCasos"] = _ferramentas.FomataNumero(cidade.casosAcumulado);
                ViewData["cidadeObitos"] = _ferramentas.FomataNumero(cidade.obitosAcumulado);

            }
            else if (!string.IsNullOrEmpty(buscaCidadeModel.NmeCidade))
            {
                PortalCidadeApiResponse cidade;

                MunicipiosServicosDadosApiResponse municipiosServicosDadosApi = _ferramentas.BuscarCidadeExata(buscaCidadeModel.NmeCidade);
                
                if(municipiosServicosDadosApi == null)
                {
                    ViewData["error"] = "Cidade não foi encontrada!";
                    return View();
                }
                try
                {
                    cidade = dadosCidade.Where(x => municipiosServicosDadosApi.id.Contains(x.cod)).FirstOrDefault();
                }
                catch
                {
                    ViewData["error"] = "Cidade não foi encontrada!";
                    return View();
                }
                if(cidade == null)
                {
                    ViewData["error"] = "Cidade não foi encontrada!";
                    return View();
                }
                ViewData["nmeCidade"] = $"{municipiosServicosDadosApi.nome}/{municipiosServicosDadosApi.microrregiao.mesorregiao.UF.sigla}";
                ViewData["cidadeCasos"] = _ferramentas.FomataNumero(cidade.casosAcumulado);
                ViewData["cidadeObitos"] = _ferramentas.FomataNumero(cidade.obitosAcumulado);
            }

            return View();
        }
    }
}
