<<<<<<< HEAD
﻿$("#register-username").focusout(function () {
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
=======
﻿$("#register-username").keyup(function () {

    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response.exists);
        if (!response.exists) {
            $("#username-ans").addClass("glyphicon-ok");
        } else {
            $("#username-ans").addClass("glyphicon-remove");
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
>>>>>>> upstream/master
        }
    }).fail(function (error) {
        console.log(error);
    });
<<<<<<< HEAD

});
$("#register-username").focusin(function () {
    $("#username-ans").removeClass("glyphicon-ok");

};
=======
});

$("#register-username").focusin(function () {
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
});
*/
>>>>>>> upstream/master
