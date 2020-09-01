using CovidMonitorAspNetCore.Code.JsonDownload;
using CovidMonitorAspNetCore.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace CovidMonitorAspNetCore.Code.Ferramentas
{
    public static class Ferramentas
    {
        public static string FomataNumero(string numero)
        {
            byte index = 1;
            char[] numeroArray = numero.ToCharArray();
            int posicaoNoArray = numeroArray.Length - 1;

            if (numeroArray.Length <=3)
            {
                return numero;
            }

            numero = string.Empty;

            foreach (var n in numeroArray)
            {
                if (index == 3)
                {
                    numero = " " + numeroArray[posicaoNoArray] + numero;
                    index = 1;
                    posicaoNoArray -= 1;
                }
                else
                {
                    numero = numeroArray[posicaoNoArray] + numero;
                    index += 1;
                    posicaoNoArray -= 1;
                }

            }
            numero = numero.Trim();
            numero = numero.Replace(" ", ".");
            return numero;
        }

        public static string BuscaUf(DadosCepApiResponse cepDados)
        {
            string municipiosJson = JsonRequest.MunicipioServicoDados();
            List<MunicipiosServicosDadosApiResponse> listaMunicipioServico = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(municipiosJson);

            var dadosMunicipio = listaMunicipioServico.Where(x => x.id == cepDados.ibge).First(); ;
            return dadosMunicipio.microrregiao.mesorregiao.UF.sigla;
        }
        public static string BuscarUf(PortalCidadeApiResponse portalCidadeApi)
        {
            string municipiosJson = JsonRequest.MunicipioServicoDados();
            List<MunicipiosServicosDadosApiResponse> listaMunicipioServico = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(municipiosJson);

            var dadosMunicipio = listaMunicipioServico.Where(x => x.nome.ToLower().Trim() == portalCidadeApi.nome.ToLower().Trim()).First();
            return dadosMunicipio.microrregiao.mesorregiao.UF.sigla;
        }
    }
}
