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
            $("#submit").attr("disabled", "disabled");
            $("#username-ans").addClass("glyphicon-remove");
        } else {
            $("#submit").removeAttr("disabled");
            $("#username-ans").addClass("glyphicon-ok");
        }
    }).fail(function (error) {
        console.log(error);
    });
});
