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
            int numero = 745;
            //Act
            var result = CovidMonitorAspNetCore.Code.Ferramentas.Ferramentas.FomataNumero(numero.ToString());
            //Assert
            result.Equals("745");
        }
    }
}
