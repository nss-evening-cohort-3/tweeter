$("#register-username").keyup(function () {
    $("#username-ans").removeClass("glyphicon-ok glyphicon-remove");
    $("#submit-msg-area").html("");
    $("form").submit(true);
    var input = $("#register-username").val();
    $.ajax({
        url: "/api/TwitUsername?candidate=" + input,
        method: 'GET'
    }).success(function (response) {
        if (!response.exists && input !== "") {
            $("form").submit(true);
            $("#register-submit").prop("disabled", false);
            $("#username-ans").addClass("glyphicon-ok");
        } else {
            $("form").submit(false);
            $("#register-submit").prop("disabled", true);
            $("#username-ans").addClass("glyphicon-remove");
            $("#submit-msg-area").html("<div id='submit-msg' style='margin-left:-89px; margin-top:8px'>Try again dude.</div>");
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