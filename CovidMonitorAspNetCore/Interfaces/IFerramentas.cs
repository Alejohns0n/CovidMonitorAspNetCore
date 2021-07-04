using CovidMonitorAspNetCore.Code.Models;

namespace CovidMonitorAspNetCore.Interfaces
{
    public interface IFerramentas
    {
        public string FomataNumero(string numero);
        public string BuscaUf(DadosCepApiResponse cepDados);
        public MunicipiosServicosDadosApiResponse BuscarCidadeExata(string nmeCidade);
    }
}
