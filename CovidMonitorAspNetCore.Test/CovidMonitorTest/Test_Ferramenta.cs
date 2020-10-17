using CovidMonitorAspNetCore.Code.Ferramentas;
using Moq;
using Xunit;

namespace CovidMonitorAspNetCore.Test.Ferramentas
{
    public class Test_Ferramenta
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

    }
}
