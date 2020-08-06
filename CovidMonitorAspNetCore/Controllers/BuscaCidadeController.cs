using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidMonitorAspNetCore.Code.Ferramentas;
using CovidMonitorAspNetCore.Code.JsonDownload;
using CovidMonitorAspNetCore.Code.Models;
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
        public string BuscarCidade(string nomeCidade)
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidades>>(dadosCidadeJsonResponse);

            foreach (var dados in dadosCidades)
            {
                if (dados.nome.ToLower() == nomeCidade.ToLower())
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
                if (dados.nome.ToLower() == nomeCidade.ToLower())
                {
                    return Ferramentas.FomataNumero(dados.obitosAcumulado);
                }
            }
            return "o nome";
        }
        public string BuscarCasos(string nomeCidade)
        {
            var dadosCidadeJsonResponse = JsonRequest.PortalCidadeRequest();
            var dadosCidades = JsonConvert.DeserializeObject<List<DadosCidades>>(dadosCidadeJsonResponse);

            foreach (var dados in dadosCidades)
            {
                if (dados.nome.ToLower() == nomeCidade.ToLower())
                {
                    return Ferramentas.FomataNumero(dados.casosAcumulado);
                }
            }
            return "da cidade!";
        }
    }
}
