using System.Net;

namespace CovidMonitorAspNetCore.Code.JsonDownload
{
    public static class XmlRequest
    {
        private const string _urlSugestaoCidades = "https://www.trabalhabrasil.com.br/api/v1.0/Cidade/List?partialDesc=";
        /// <summary>
        /// Retorna um xml com sujestão de dez cidades pelo qual a pessoa ta procurando.
        /// </summary>
        /// <param name="nmCidadePesquisa"></param>
        /// <returns></returns>
        public static string XmlSugestaoCidades(string nmCidadePesquisa)
        {
            var client = new WebClient();
            return client.DownloadString($"{_urlSugestaoCidades}{nmCidadePesquisa}");
        }
    }
}
