using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMonitorAspNetCore.Code.Models
{
    public class PortalCidadeApiResponse
    {
        public class DadosCidades
        {
            public string _id { get; set; }
            public string nome { get; set; }
            public string cod { get; set; }
            public string casosAcumulado { get; set; }
            public string obitosAcumulado { get; set; }
        }

        public class RootJsonPortalCidade
        {
            public List<DadosCidades> Dados { get; set; }
        }

    }
}
