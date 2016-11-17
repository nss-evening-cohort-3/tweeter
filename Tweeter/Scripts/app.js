$("#register-username").keyup(function () {
    $("#username-ans").removeClass("glyphicon-ok");
    $("#username-ans").removeClass("glyphicon-remove");
    console.log("before");
    return true;
    console.log("after");
    $.ajax({
        url: "/api/TwitUsername?candidate=" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response.exists);
        if (!response.exists) {
            console.log("username does not exist")
            $("#username-ans").addClass("glyphicon-ok");
            $("#submitbtn").removeClass("disabled");
        } else {
            console.log("username does exist")
            $("#username-ans").addClass("glyphicon-remove");
            $("#submitbtn").addClass("disabled");
            $("form").submit(function (event) {
                event.preventDefault();
            })
        }
    }).fail(function (error) {
        console.log(error);
    });
});

