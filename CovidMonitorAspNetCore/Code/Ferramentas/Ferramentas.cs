﻿using CovidMonitorAspNetCore.Code.JsonDownload;
using CovidMonitorAspNetCore.Code.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CovidMonitorAspNetCore.Code.Ferramentas
{
    /// <summary>
    /// Classe com ferramentas utilitarias.
    /// </summary>
    public static class Ferramentas
    {
        ///<summary>
        ///Adiciona ponto flutuante em numeros inteiros.
        ///</summary>
        ///<returns>
        ///Uma string com o número formatado.
        /// </returns>
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
            numero = numero.Trim().Replace(" ", ".");
            return numero;
        }

        public static string BuscaUf(DadosCepApiResponse cepDados)
        {
            string municipiosJson = JsonRequest.MunicipioServicoDados();
            List<MunicipiosServicosDadosApiResponse> listaMunicipioServico = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(municipiosJson);

            var dadosMunicipio = listaMunicipioServico.Where(x => x.id == cepDados.ibge).First(); 
            if (string.IsNullOrEmpty(dadosMunicipio.microrregiao.mesorregiao.UF.sigla))
                return "";
            else
                return dadosMunicipio.microrregiao.mesorregiao.UF.sigla;
        }
        /// <summary>
        /// Busca a cidade exata que a pessoa esta fazendo a busca.
        /// </summary>
        /// <param name="portalCidadeApi"></param>
        /// <returns></returns>
        public static MunicipiosServicosDadosApiResponse BuscarCidadeExata(string nmeCidade)
        {
            string municipiosJson = JsonRequest.MunicipioServicoDados();
            List<MunicipiosServicosDadosApiResponse> listaMunicipioServico = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(municipiosJson);

            Regex regex = new Regex(@"(?<=\/).*");
            Regex matchCidade = new Regex(@".*(\/)");
            string uf = regex.Match(nmeCidade).ToString().Trim();
            string cidade = matchCidade.Match(nmeCidade).ToString().Replace("/", "");

            MunicipiosServicosDadosApiResponse dadosMunicipio = listaMunicipioServico.Where(x => x.nome.ToLower().Trim() == cidade.ToLower().Trim() & uf.ToLower().Trim() == x.microrregiao.mesorregiao.UF.sigla.ToLower().Trim()).FirstOrDefault();

            return dadosMunicipio ;

            //var a = seIssoForNull ?? recebeIsso;
        }

        public static async Task<List<string>> ListaSugestaoCidades()
        {
            List<string> listaCidades = new List<string>();
            string jsonCidadesBrasil = JsonRequest.MunicipioServicoDados();
            List<MunicipiosServicosDadosApiResponse> municipiosResponse = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(jsonCidadesBrasil);
            foreach (var cidade in municipiosResponse)
                listaCidades.Add($"{cidade.nome}/{cidade.microrregiao.mesorregiao.UF.sigla}");

            return listaCidades;
        }
    }
}
