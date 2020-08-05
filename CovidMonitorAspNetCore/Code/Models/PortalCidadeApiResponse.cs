using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMonitorAspNetCore.Code.Models
{
    public class PortalCidadeApiResponse
    {
        public class DadosCidade
        {
            public string _id { get; set; }
            public string nome { get; set; }
            public string cod { get; set; }
            public int casosAcumulado { get; set; }
            public int obitosAcumulado { get; set; }
        }

        public class RootJsonPortalCidade
        {
            public List<DadosCidade> Dados { get; set; }
        }

    }
}
