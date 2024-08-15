$(function () {
    if (window.mostrarModal) {
        $('#Modal').modal('show');
        setTimeout(function () {
            $('#Modal').modal('hide');
        }, 5000);
    }

    
    if (window.mostrarAlerta) {
        $('#Alerta').fadeIn(); // Exibe o alerta com efeito de desvanecimento
        setTimeout(function () {
            $('#Alerta').fadeOut(); // Oculta o alerta com efeito de desvanecimento
        }, 5000); // 3500 milissegundos = 3,5 segundos
    }   


    $('#cpfForm').on('submit', function (event) {
     
        console.log("Modal CPF carregando...");

        if (window.mostrarCarregando) {
            $('#modal-loading').modal('show');
        }
        else {
            $('#modal-loading').modal('hide');
        }
    });


    $('#nomeForm').on('submit', function (event) {

        console.log("Modal Nome carregando...");

        if (window.mostrarCarregando) {
            $('#modal-loading').modal('show');
        }
        else {
            $('#modal-loading').modal('hide');
        }

    });

});




function processCPF(input) {
    console.log("processCPF...");
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

function processNome(input) {
    console.log("processNome...");
    let value = input.value;
    let filteredValue = value.replace(/[^\p{L}\s]/gu, "");


    if (filteredValue.length > 100) {
        filteredValue = filteredValue.slice(0, 100);
    }
    input.value = filteredValue;

    // Verifica se o comprimento está dentro dos limites
    if (filteredValue.length < input.minLength) {
        input.setCustomValidity(`O valor deve ter pelo menos ${input.minLength} caracteres.`);
    } else {
        input.setCustomValidity('');
    }
}

function validateFormCPF(event) {

    console.log("validateFormCPF...");
    let value = event.value;
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

function validateFormNome(event) {

    console.log("validateFormNome...");

    let value = input.value;
    let filteredValue = value.replace(/[^\p{L}\s]/gu, "");

    input.value = filteredValue;

    // Verifica se o comprimento está dentro dos limites
    if (input.length < input.minLength) {
        input.setCustomValidity(`O valor deve ter pelo menos ${input.minLength} caracteres.`);
    } else if (input.length > 50) {
        input.setCustomValidity(`O valor deve ter no maximo ${input.minLength} caracteres.`);
    } else {
        input.setCustomValidity('');
    }
}





