using CovidMonitorAspNetCore.Code.Ferramentas;
using System;
using Xunit;

namespace Teste
{
    public class Teste
    {
        [Fact]
        public void FormatarNumero()
        {
            //Arranje
            string numero = "745123";
            //Act
            var result = Ferramentas.FomataNumero(numero);
            //Assert
            result.Equals("745.123");
        }
        
    }
}
