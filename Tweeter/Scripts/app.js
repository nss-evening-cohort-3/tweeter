$("#register-username").keyup(function () {
    (console.log("key up"))
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    $("#username-ans"). removeClass("disabled");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response.exists);
        if (!response.exists) {
            $("#username-ans").addClass("glyphicon-ok");
        } else {
            $("#username-ans").addClass("glyphicon-remove");
            $("#sendForm").addClass("disabled, disabled");
        }
    }).fail(function (error) {
        console.log(error);
    });
});


/*
$("#register-username").focusout(function () {
    //alert("defocused!!!");
    //console.log($(this).val());
    //$("#username-ans").addClass("hidden");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response);
        if (response.exists) {
            $("#username-ans").addClass("glyphicon-ok");
        } else {
            $("#username-ans").addClass("glyphicon-remove");
        }
    }).fail(function (error) {
        console.log(error);
    });
});

$("#register-username").focusin(function () {
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
});
*/