$("#register-username").keyup(function (e) {

    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response.exists);
        if (!response.exists) {
            $("#username-ans").addClass("glyphicon-ok");

            $("#RegisterButton").attr("disabled", false);
            console.log("username did not exist in database");
            $('#RegisterButton').show();

        } else {
            $("#username-ans").addClass("glyphicon-remove");
            
            $("input[RegisterButton]").attr("disabled", true);
            $("input[RegisterButton]").prop("disabled", true);
            $('#RegisterButton').hide();
            e.preventDefault();

            console.log("username exist in database");
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