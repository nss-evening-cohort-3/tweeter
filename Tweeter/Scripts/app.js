var userNameField = $('#register-username');
userNameField.focusout(function () {
    $.ajax({
        method: 'GET',
        url: 'api/Register'
    })
});

