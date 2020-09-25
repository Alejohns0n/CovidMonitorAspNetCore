
function VerificarCampos(campoPreenchido) {

    if (campoPreenchido == "cidade") {
        document.getElementById('cep').value = ("");
    }

    if (campoPreenchido =="cep") {
        document.getElementById('nmecidade').value = ("");
    }
}

function SujestCidade(value) {
    return null;
    //ate eu arrumar o bug do fetch, eu poderiapegar a respostaa direto de uma api, mas quero pegar da controller.
    VerificarCampos("cidade");
    fetch('/BuscaCidade/ListaSugestoesDeCidades', {
        method: 'GET',
        data:{
            nmeCidadePesquisa: value
        }
    }).then(response => response.text()).then(listaCidades => {
        console.log(listaCidades);
    });

    /*A função fetch devolve uma Promise e por padrão faz uma requisição do tipo get(pode ser mudado para qualquer verbo), assim precisamos tratá - la
    /para sucesso e possíveis erros, para tratar o sucesso(success) podemos utilizar o.then e passar a resposta(response) como parâmetro para ele.*/        
}