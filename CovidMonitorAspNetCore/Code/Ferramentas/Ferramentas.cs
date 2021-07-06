using CovidMonitorAspNetCore.Code.Models;
using CovidMonitorAspNetCore.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CovidMonitorAspNetCore.Code.Ferramentas
{
    /// <summary>
    /// Classe com ferramentas utilitarias.
    /// </summary>
    public class Ferramentas : IFerramentas
    {
        private IJsonRequest _jsonRequest;

        public Ferramentas(IJsonRequest jsonRequest)
        {
            _jsonRequest = jsonRequest;
        }

        ///<summary>
        ///Adiciona ponto flutuante em numeros inteiros.
        ///</summary>
        ///<returns>
        ///Uma string com o número formatado.
        /// </returns>
        public string FomataNumero(string numero)
        {
            byte index = 1;
            char[] numeroArray = numero.ToCharArray();
            int posicaoNoArray = numeroArray.Length - 1;

            if (numeroArray.Length <= 3)
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

        public string BuscaUf(DadosCepApiResponse cepDados)
        {
            string municipiosJson = _jsonRequest.MunicipioServicoDados();
            List<MunicipiosServicosDadosApiResponse> listaMunicipioServico = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(municipiosJson);

            var dadosMunicipio = listaMunicipioServico.Where(x => x.id == cepDados.ibge).First();
            if (string.IsNullOrEmpty(dadosMunicipio.uf))
                return "";
            else
                return dadosMunicipio.uf;
        }
        /// <summary>
        /// Busca a cidade exata que a pessoa esta fazendo a busca.
        /// </summary>
        /// <param name="portalCidadeApi"></param>
        /// <returns></returns>
        public MunicipiosServicosDadosApiResponse BuscarCidadeExata(string nmeCidade)
        {
            string uf = nmeCidade.Split("/").Last().ToLower().Trim();
            nmeCidade = nmeCidade.Split("/").First().ToLower().Trim();

            List<MunicipiosServicosDadosApiResponse> listaMunicipioServico = JsonConvert.DeserializeObject<List<MunicipiosServicosDadosApiResponse>>(_jsonRequest.MunicipioServicoDados());
            return listaMunicipioServico.Where(
                x => x.nome.ToLower().Trim() == nmeCidade &
                x.uf.ToLower().Trim() == uf).FirstOrDefault();
        }
    }
}
