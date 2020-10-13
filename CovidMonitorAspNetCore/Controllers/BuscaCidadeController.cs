using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                if (Regex.IsMatch(buscaCidadeModel.Cep, @"[aA-zA]"))
                {
                    ViewData["error"] = "Caracteres inválidos no CEP!";
                    return View();
                }
                var jsonCepDados = JsonRequest.CepRequest(buscaCidadeModel.Cep.Trim().Replace("-", ""));
                if (jsonCepDados.Contains("erro"))
                {
                    ViewData["error"] = "Cep não encontrado";
                    return View();
                }
                DadosCepApiResponse dadosCep = JsonConvert.DeserializeObject<DadosCepApiResponse>(jsonCepDados);
                PortalCidadeApiResponse cidade = dadosCidade.Where(x => dadosCep.ibge.Contains(x.cod)).First();

                ViewData["nmeCidade"] = $"{cidade.nome}/{dadosCep.uf}";
                ViewData["cidadeCasos"] = Ferramentas.FomataNumero(cidade.casosAcumulado);
                ViewData["cidadeObitos"] = Ferramentas.FomataNumero(cidade.obitosAcumulado);

            }
            else if (!string.IsNullOrEmpty(buscaCidadeModel.NmeCidade))
            {
                PortalCidadeApiResponse cidade;

                MunicipiosServicosDadosApiResponse municipiosServicosDadosApi = Ferramentas.BuscarCidadeExata(buscaCidadeModel.NmeCidade);
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
                ViewData["cidadeCasos"] = Ferramentas.FomataNumero(cidade.casosAcumulado);
                ViewData["cidadeObitos"] = Ferramentas.FomataNumero(cidade.obitosAcumulado);
            }

            return View();
        }

        public string ListaSugestoesDeCidades(string nmeCidadePesquisa)
        {
            string cidade = string.Empty;
            string xmlCidadesSugestao = XmlRequest.XmlSugestaoCidades(nmeCidadePesquisa);
            List<CidadeSugestao> listCidadeSujestao = JsonConvert.DeserializeObject<List<CidadeSugestao>>(xmlCidadesSugestao);

            foreach (CidadeSugestao cidadeDados in listCidadeSujestao)
                cidade = cidade + "|||" + cidadeDados.descricao;

           cidade = cidade.Replace("|||", "  ").Trim();
            cidade = cidade.Replace("  ", "|||");
            return cidade;
        }
    }
}
