using CovidMonitorAspNetCore.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;
using System.Text;

namespace CovidMonitorAspNetCore.Code.JsonDownload
{
    /// <summary>
    /// Os metodos dessa classe fazem um request na api que retorna uma resposta em json.
    /// </summary>
    public class JsonRequest : IJsonRequest
    {
        private IMemoryCache _memoryCache;

        private static string _portalEstadoUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalEstado";
        private static string _portalGeralUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalGeralApi";
        private static string _portalCidadeUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalMunicipio";
        private static string _CepApiUrl = "https://viacep.com.br/ws/";
        private static string _MunicipiosServicoDadosUrl = "wwwroot/data/cidadesibge.json";

        public JsonRequest(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Retorna o json da api com dados gerais sobre o covid-19.
        /// </summary>
        public string PortalGeralRequest()
        {
            if (_memoryCache.TryGetValue("portalGeralRequest", out byte[] jsonByte))
                return Encoding.ASCII.GetString(jsonByte);
            else
            {
                var client = new WebClient();
                string json = client.DownloadString(_portalGeralUrl);
                _memoryCache.Set("portalGeralRequest", Encoding.ASCII.GetBytes(json), SetMemoryOptions(1200, 300));
                return json;
            }
        }

        /// <summary>
        /// Retorna o json da api com dados de cada estado do Brasil.
        /// </summary>
        public string PortalEstadoRequest()
        {
            if (_memoryCache.TryGetValue("portalEstadoRequest", out byte[] jsonByte))
                return Encoding.ASCII.GetString(jsonByte);
            else
            {
                var client = new WebClient();
                string json = client.DownloadString(_portalEstadoUrl);
                _memoryCache.Set("portalEstadoRequest", Encoding.ASCII.GetBytes(json), SetMemoryOptions(1200, 300));
                return json;
            }

        }

        /// <summary>
        /// Retorna o json com dados sobre o covid-19 em cada municipio do Brasil.
        /// </summary>
        public string PortalCidadeRequest()
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(_portalCidadeUrl);
            }
        }

        /// <summary>
        /// Retorna o Códigos de Endereçamento Postal (CEP) do Brasil.
        /// </summary>
        /// <param name="cep"></param>
        public string CepRequest(string cep)
        {
            //nao esta sendo usado
            if (_memoryCache.TryGetValue(cep, out string json))
                return json;
            else
            {
                var client = new WebClient();
                json = client.DownloadString($"{_CepApiUrl}{cep}/json/");
                _memoryCache.Set(cep, json, SetMemoryOptions(1200, 300));
                return json;
            }
        }

        /// <summary>
        /// Retornar um json com um conjunto de municípios do Brasil
        /// </summary>
        public string MunicipioServicoDados()
        {
            using(var client = new WebClient())
            {
                return client.DownloadString(_MunicipiosServicoDadosUrl);
            }
        }


        private MemoryCacheEntryOptions SetMemoryOptions(double absoluteExpiration, double slidingExpiration)
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpiration),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpiration)
            };
        }
    }
}
