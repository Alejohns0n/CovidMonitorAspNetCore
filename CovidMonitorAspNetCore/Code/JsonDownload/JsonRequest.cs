using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CovidMonitorAspNetCore.Code.JsonDownload
{
    public abstract class JsonRequest
    {
        private static string _portalEstadoUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalEstado";
        private static  string _portalGeralUrl = "https://xx9p7hp1p7.execute-api.us-east-1.amazonaws.com/prod/PortalGeralApi";
        public static string PortalGeralRequest()
        {
            var client = new WebClient();
            string json = client.DownloadString(_portalGeralUrl);
            return json;
        }

        public static string PortalEstadoRequest()
        {
            var client = new WebClient();
            string json = client.DownloadString(_portalEstadoUrl);
            return json;
        }
    }
}
