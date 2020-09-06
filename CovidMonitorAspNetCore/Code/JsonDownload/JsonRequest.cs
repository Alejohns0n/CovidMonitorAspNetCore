using System.Net;

namespace CovidMonitorAspNetCore.Code.JsonDownload
{/// <summary>
/// Os metodos dessa classe fazem um request na api que retorna uma resposta em json.
/// </summary>
    public abstract class JsonRequest
    {
        private static string _portalEstadoUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalEstado";
        private static string _portalGeralUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalGeralApi";
        private static string _portalCidadeUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalMunicipio";
        private static string _CepApiUrl = "https://viacep.com.br/ws/";
        private static string _MunicipiosServicoDadosUrl = "https://servicodados.ibge.gov.br/api/v1/localidades/municipios";
        
        /// <summary>
        /// Retorna o json da api com dados gerais sobre o covid-19.
        /// </summary>
        public static string PortalGeralRequest()
        {
            var client = new WebClient();
            return client.DownloadString(_portalGeralUrl);
        }

        /// <summary>
        /// Retorna o json da api com dados de cada estado do Brasil.
        /// </summary>
        public static string PortalEstadoRequest()
        {
            var client = new WebClient();
            return client.DownloadString(_portalEstadoUrl);
        }

        /// <summary>
        /// Retorna o json com dados sobre o covid-19 em cada municipio do Brasil.
        /// </summary>
        public static string PortalCidadeRequest()
        {
            var client = new WebClient();
            return client.DownloadString(_portalCidadeUrl);
        }

        /// <summary>
        /// Retorna o Códigos de Endereçamento Postal (CEP) do Brasil.
        /// </summary>
        /// <param name="cep"></param>
        public static string CepRequest(string cep)
        {
            var client = new WebClient();
            return client.DownloadString($"{_CepApiUrl}{cep}/json/");
        }
        /// <summary>
        /// Retornar um json com um conjunto de municípios do Brasil
        /// </summary>
        public static string MunicipioServicoDados()
        {
            var client = new WebClient();
            return client.DownloadString(_MunicipiosServicoDadosUrl);
        }
    }
}
