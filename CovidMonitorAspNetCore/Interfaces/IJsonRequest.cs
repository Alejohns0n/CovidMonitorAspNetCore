namespace CovidMonitorAspNetCore.Interfaces
{
    public interface IJsonRequest
    {
        public string PortalGeralRequest();
        public string PortalEstadoRequest();
        public string PortalCidadeRequest();
        public string CepRequest(string cep);
        public string MunicipioServicoDados();
    }
}
