$("#register-username").focusout(function () {
    // alert("defocused!!!"); this is correct so far
    console.log($(this).val());
   // $(this).val();//this refers to #register-username; this refers to current element you are sitting at
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),

        method: 'GET'
    }).success(function (response) {
        console.log(response);
        if(response.exists){
            $("username-ans").addClass("glyphicon-ok");
        } else {
            $("username-ans").addClass("glyphicon-remove");
        }
    }).fail(function (error) {
        console.log(error);
    });

});
$("#register-username").focusin(function () {
    $("#username-ans").removeClass("glyphicon-ok");

};