$("#register-username").keyup(function () {

    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    $("input[type='submit']").removeClass('disabled');
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response.exists);
        if (!response.exists) {
            $("#username-ans").addClass("glyphicon-ok");
        } else {
            $("#username-ans").addClass("glyphicon-remove");
            $("input[type='submit']").addClass('disabled');
        }
    }).fail(function (error) {
        console.log(error);
    });
});
