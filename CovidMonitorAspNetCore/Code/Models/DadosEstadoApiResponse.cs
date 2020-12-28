namespace CovidMonitorAspNetCore.Code.Models
{
    public class DadosEstadoApiResponse
    {
        public string _id { get; set; }
        public string nome { get; set; }
        public string casosAcumulado { get; set; }
        public string obitosAcumulado { get; set; }
        public string populacaoTCU2019 { get; set; }
        public string incidencia { get; set; }
        public string incidenciaObito { get; set; }
    }
}
