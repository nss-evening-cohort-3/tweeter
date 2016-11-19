﻿let showUsernameInputValidStyling = () => {
    $('#username-help-block').addClass('hidden-xs-up');
    $('#register-username').addClass('form-control-success');
    $('.form-group__input--username').addClass('has-success');
};
let showUsernameInputErrorStyling = () => {
    $('#username-help-block').removeClass('hidden-xs-up');
    $('#register-username').addClass('form-control-danger');
    $('.form-group__input--username').addClass('has-danger');
};

let disableRegisterSubmitButton = () => {
    $('.button__registration--submit').addClass('disabled btn-danger');
    $('.button__registration--submit').attr('disabled', 'disabled');
};

let enableRegisterSubmitButton = () => {
    $('.button__registration--submit').removeClass('disabled btn-danger');
    $('.button__registration--submit').removeAttr('disabled');
};

$("#register-username").focusin(function () {
    $('#username-help-block').show();
});

$("#register-username").focusout(function () {
    $('#username-help-block').hide();
});

$("#register-username")
    .keyup(function () {
        $('.form-group__input--username').removeClass('has-success has-danger');
        $.ajax({
            url: '/api/TwitUsername?candidate=' + $(this).val(),
            method: 'GET'
        }).done(function (response) {
            if (!response.exists) {
                showUsernameInputValidStyling();
                enableRegisterSubmitButton();
            } else {
                showUsernameInputErrorStyling();
                disableRegisterSubmitButton();
            }
        }).fail(function (error) {
            console.log(error);
        });
    });
