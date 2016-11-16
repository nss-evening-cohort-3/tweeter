$("#sss").focusout(function () {
    console.log("wwwww" + $(this).val());
    $.ajax({
        url: "/api/TwitUsername/" + $(this).val(),
        method: 'GET'
    }).success(function (response) {
        console.log(response);
    }).fail(function (error) {
        console.log("Failed");
    });
});