
function VerificarCampos(campoPreenchido) {

    if (campoPreenchido == "cidade") {
        document.getElementById('cep').value = ("");
    }

    if (campoPreenchido =="cep") {
        document.getElementById('nmecidade').value = ("");
    }
}

function SujestCidade() {
    VerificarCampos("cidade");
    var nmeCidade = document.getElementById('cidade').value;

    var listaCidades2;
    var cidadesSujest = $.ajax({
        url: '/BuscaCidade/ListaSugestoesDeCidades',
        data: {
            nmeCidadePesquisa: nmeCidade
        }
    }).done(function (listaCidades) {
        listaCidades2 = listaCidades.split("|||");
        $("#cidade").autocomplete({
            source: listaCidades2
        });
    });
}