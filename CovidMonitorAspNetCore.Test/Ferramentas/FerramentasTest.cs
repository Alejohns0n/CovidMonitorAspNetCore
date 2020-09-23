using Xunit;

namespace CovidMonitorAspNetCore.Test.Ferramentas
{
    public class FerramentasTest
    {
        [Fact]
        public void FormatarNumero()
        {
            //Arranje
            string numero = "745123";
            //Act
            var result = Code.Ferramentas.Ferramentas.FomataNumero(numero);
            //Assert
            result.Equals("745.123");
        }

        [Fact]
        public void TestarXml()
        {
            string nmeCidade = "belem";

            var result = Code.JsonDownload.XmlRequest.XmlSugestaoCidades(nmeCidade);

            result.Equals("");
        }
        [Fact]
        public void TestarJsonPortalgeral()
        {
            var result = Code.JsonDownload.JsonRequest.PortalCidadeRequest();

            var a = 10;
        }

    }
}
