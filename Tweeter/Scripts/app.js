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
            $('username-ans').addClass("glyphicon-ok");
        } else {
            $('username-ans').addClass("glyphicon-remove")
        }
    }).fail(function (error) {
        console.log('Failed!' + error)
    });
});

