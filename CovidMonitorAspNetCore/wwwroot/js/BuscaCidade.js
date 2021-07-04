

function VerificarCampos(campoPreenchido) {

    if (campoPreenchido == "cidade") {
        document.getElementById('cep').value = ("");
    }

    if (campoPreenchido == "cep") {
        document.getElementById('cidade').value = ("");
    }
}


var input = document.querySelector(".input");
var list = document.querySelector(".list");
var arrow = document.querySelector(".arrow-up");
var data_list = input.getAttribute("data-list");
var selecting = -1;
var list_li;

// Make data list array
data_list = data_list.split(",");

var typing_arr = [];

input.addEventListener("keyup", function (e) {
    list.innerHTML = '';
    var typing = data_list;

    $.ajax({
        url: "https://www.trabalhabrasil.com.br/api/v1.0/Cidade/List?partialDesc=" + e.target.value.toLowerCase(),
    }).done(function (response) {
        var listaCidades = response.map(function (item) { return item.descricao });
        console.log('lista cidades', listaCidades);
        typing = listaCidades.filter(function (item, indice) {
            return item.toLowerCase().search(e.target.value.toLowerCase()) != -1;
        });
        console.log('typing', typing);
        console.log('typing arr', typing_arr);

        // If value empty hide list and arrow
        if (e.target.value === "" || typing.length === 0) {
            list.classList.add("hide");
            list.classList.remove("show");

            arrow.classList.add("hide");
            arrow.classList.remove("show");
        } else {
            list.classList.remove("hide");
            list.classList.add("show");

            arrow.classList.remove("hide");
            arrow.classList.add("show");
        }

        typing_arr = typing;

        // Show filtered data-list
        typing_arr.map(function (list_item) {
            var li = document.createElement("li");
            list.appendChild(li);
            li.innerHTML = list_item;

            // Close list when click on li and make clicked li input value
            li.addEventListener("click", function (e) {
                input.value = e.target.textContent;
                list.classList.add("hide");
                arrow.classList.add("hide");
            });

            // Close list when click enter and make selecting li input value
            if (e.which == 13 || e.keyCode == 13) {
                if (selecting > -1) {
                    input.value = list_li[selecting].textContent;
                    list.classList.add("hide");
                    arrow.classList.add("hide");
                }
            }

        });

        // Down and up buttons
        if (e.which == 40 || e.keyCode == 40) {
            selecting++;

            list_li = list.querySelectorAll("li")
            if (selecting + 1 > list_li.length) {
                selecting = 0;
            }

            list_li[selecting].classList.add("selecting");

            // When input value change, make selecting -1
            input.addEventListener("input", function (e) {
                selecting = -1;
            });

        } else if (e.which == 38 || e.keyCode == 38) {
            selecting--;

            list_li = list.querySelectorAll("li")
            if (selecting < 0) {
                selecting = list_li.length - 1;
            }

            list_li[selecting].classList.add("selecting");
        }

        // Keep selecting class when press a button
        list_li = list.querySelectorAll("li")
        list_li[selecting].classList.add("selecting");
    });
});