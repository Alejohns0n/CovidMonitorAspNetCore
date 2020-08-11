using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CovidMonitorAspNetCore.Code.Models
{
    public class PortalGeralApiResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Confirmados
        {
            public string total { get; set; }
            public string titulo { get; set; }
            public int novos { get; set; }
            public string incidencia { get; set; }
            public string recuperados { get; set; }
            public string acompanhamento { get; set; }
            public string percent { get; set; }
        }

        public class Obitos
        {
            public string total { get; set; }
            public string titulo { get; set; }
            public int novos { get; set; }
            public string incidencia { get; set; }
            public string letalidade { get; set; }
            public string mortalidade { get; set; }
            public string percent { get; set; }
        }

        public class Arquivo
        {
            public string __type { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Planilha
        {
            public string usuario { get; set; }
            public string nome { get; set; }
            public string usuario_id { get; set; }
            public string status { get; set; }
            public Arquivo arquivo { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
            public string objectId { get; set; }
        }

        public class Root
        {
            public Confirmados confirmados { get; set; }
            public Obitos obitos { get; set; }
            public DateTime dt_updated { get; set; }
            public Planilha planilha { get; set; }
        }


    }
}
