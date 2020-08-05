function MostrarCidade(nmeCidadeDigitado) {
    var x = nmeCidadeDigitado
    var buscarCidade = $.ajax({
        url: '/BuscaCidade/BuscarCidade',
        data: {
            nomeCidade: nmeCidadeDigitado
        }
    });
}