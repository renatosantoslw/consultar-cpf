$(document).ready(function () {
    if (window.mostrarModal) {
        $('#Modal').modal('show');
        setTimeout(function () {
            $('#Modal').modal('hide');
        }, 8000);
    }


    if (window.mostrarAlerta) {
        $('#Alerta').fadeIn(); // Exibe o alerta com efeito de desvanecimento
        setTimeout(function () {
            $('#Alerta').fadeOut(); // Oculta o alerta com efeito de desvanecimento
        }, 8000); // 3500 milissegundos = 3,5 segundos
    }       
});


function process(input) {
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;

    // Verifica se o comprimento está dentro dos limites
    if (numbers.length < input.minLength) {
        input.setCustomValidity(`O valor deve ter pelo menos ${input.minLength} caracteres.`);
    } else if (numbers.length > 11) {
        input.setCustomValidity(`O valor deve ter no maximo ${input.minLength} caracteres.`);
    } else {
        input.setCustomValidity('');
    }

}

function validateForm(event) {

    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;

    // Verifica se o comprimento está dentro dos limites
    if (numbers.length < input.minLength) {
        input.setCustomValidity(`O valor deve ter pelo menos ${input.minLength} caracteres.`);
    } else if (numbers.length > 11) {
        input.setCustomValidity(`O valor deve ter no maximo ${input.minLength} caracteres.`);
    } else {
        input.setCustomValidity('');
    }
}



