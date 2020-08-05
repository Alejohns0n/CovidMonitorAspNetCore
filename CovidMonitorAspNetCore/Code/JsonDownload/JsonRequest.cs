using System.Net;

namespace CovidMonitorAspNetCore.Code.JsonDownload
{
    public abstract class JsonRequest
    {
        private static string _portalEstadoUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalEstado";
        private static string _portalGeralUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalGeralApi";
        private static string _portalCidadeUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalMunicipio";
        public static string PortalGeralRequest()
        {
            var client = new WebClient();
            return client.DownloadString(_portalGeralUrl);
        }

        public static string PortalEstadoRequest()
        {
            var client = new WebClient();
            return client.DownloadString(_portalEstadoUrl);
        }

        public static string PortalCidadeRequest()
        {
            var client = new WebClient();
            return client.DownloadString(_portalCidadeUrl);
        }
    }
}
