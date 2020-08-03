using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CovidMonitorAspNetCore.Models
{
    public class DadosEstadoApiResponse
    {
        public class MyArray
        {
            public string _id { get; set; }
            public string nome { get; set; }
            public string casosAcumulado { get; set; }
            public string obitosAcumulado { get; set; }
            public string populacaoTCU2019 { get; set; }
            public string incidencia { get; set; }
            public string incidenciaObito { get; set; }
        }

        public class RootEstado
        {
            public List<MyArray> MyArray { get; set; }
        }
    }
}
