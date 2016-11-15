$("#register-username").keyup(function () {

    // first step is removing a UI reponse to the AJAX call by taking out classes
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    // get value from username field input
    var input = $("#register-username").val();
    $.ajax({
        url: "/api/TwitUsername?candidate=" + input,
        method: 'GET'
    }).success(function (response) {
        //console.log(response.exists);
        //added stuff so that field can't be empty
        if (!response.exists && input !== "") {
            $("#username-ans").addClass("glyphicon-ok");
            $("#register-submit").prop("disabled", false);
        } else {
            $("#username-ans").addClass("glyphicon-remove");
            $("#register-submit").prop("disabled", true);
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