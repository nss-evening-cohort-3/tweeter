var userNameField = $('#register-username');
userNameField.keyup(function () {

})
userNameField.focusout(function () {
    console.log($(this).val())
    $.ajax({
        url: '/api/TwitUsername?candidate='+$(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response);
        if (response.exists) {
            $('#username-ans').addClass("glyphicon-remove");
            $('#username-ans').removeClass("glyphicon-ok");
            $('input[value="Register"]').addClass("disabled").removeClass("btn-info");

        } else {
            $('#username-ans').addClass("glyphicon-ok");
            $('#username-ans').removeClass("glyphicon-remove ");
            $('input[value="Register"]').removeClass("disabled").addClass("btn-info");
        }
    }).fail(function (error) {
        console.log('Failed!' + error)
    });
});

