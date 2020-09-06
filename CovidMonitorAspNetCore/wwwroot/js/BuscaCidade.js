function MostrarCidade(nmeCidadeDigitado) {
    PegarCidade(nmeCidadeDigitado);
    PegarCasos(nmeCidadeDigitado);
    PegarObitos(nmeCidadeDigitado);   
}

function PegarCidade(nmeCidadeDigitado) {
    var buscarCidade = $.ajax({
        url: '/BuscaCidade/BuscarCidade',
        data: {
            nomeCidade: nmeCidadeDigitado
        }
    });

    buscarCidade.done(function (retorno) {
        document.getElementById("cidade").innerHTML = retorno;
    });
}

function PegarObitos(nmeCidadeDigitado) {
    var buscarCidade = $.ajax({
        url: '/BuscaCidade/BuscarObitos',
        data: {
            nomeCidade: nmeCidadeDigitado
        }
    });

    buscarCidade.done(function (retorno) {
        document.getElementById("obitos").innerHTML = retorno;
    });
}

function PegarCasos(nmeCidadeDigitado) {
    var buscarCidade = $.ajax({
        url: '/BuscaCidade/BuscarCasos',
        data: {
            nomeCidade: nmeCidadeDigitado
        }
    });

    buscarCidade.done(function (retorno) {
        document.getElementById("casos").innerHTML = retorno;
    });

}
