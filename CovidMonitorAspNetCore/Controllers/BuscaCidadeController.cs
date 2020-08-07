using System.Collections.Generic;
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
            return View();
        }
        public string BuscarCidade(string nomeCidade)
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidades>>(dadosCidadeJsonResponse);

            foreach (var dados in dadosCidades)
            {
                if (dados.nome.ToLower() == nomeCidade.ToLower().Trim())
                {
                    return dados.nome;
                }
            }
            return "Digite corretamente";
        }
        public string BuscarObitos(string nomeCidade)
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidades>>(dadosCidadeJsonResponse);

            foreach (var dados in dadosCidades)
            {
                if (dados.nome.ToLower() == nomeCidade.ToLower().Trim())
                {
                    return Ferramentas.FomataNumero(dados.obitosAcumulado);
                }
            }
            return "da cidade!";
        }
        public string BuscarCasos(string nomeCidade)
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidades>>(dadosCidadeJsonResponse);

            foreach (var dados in dadosCidades)
            {
                if (dados.nome.ToLower() == nomeCidade.ToLower().Trim())
                {
                    return Ferramentas.FomataNumero(dados.casosAcumulado);
                }
            }
            return "o nome";
        }
    }
}
