
function VerificarCampos(campoPreenchido) {

    if (campoPreenchido == "cidade") {
        document.getElementById('cep').value = ("");
    }

    if (campoPreenchido == "cep") {
        document.getElementById('cidade').value = ("");
    }
}
