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

function VerificarCampos(campoPreenchido) {

    if (campoPreenchido == "nmecidade") {
        document.getElementById('cep').value = ("");
    }

    if (campoPreenchido =="cep") {
        document.getElementById('nmecidade').value = ("");
    }
}