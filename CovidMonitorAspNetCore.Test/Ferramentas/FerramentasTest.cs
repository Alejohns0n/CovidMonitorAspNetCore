using CovidMonitorAspNetCore.Code.Ferramentas;
using System;
using System.Collections.Generic;
using System.Text;
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
            var result = CovidMonitorAspNetCore.Code.Ferramentas.Ferramentas.FomataNumero(numero);
            //Assert
            result.Equals("745.123");
        }
    }
}
